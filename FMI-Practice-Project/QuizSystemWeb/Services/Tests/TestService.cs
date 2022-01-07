namespace QuizSystemWeb.Services.Tests
{
    using Newtonsoft.Json;
    using QuizSystemWeb.Data;
    using QuizSystemWeb.Data.Entities;
    using QuizSystemWeb.Services.Questions.Models;
    using QuizSystemWeb.Services.Tests.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TestService : ITestService
    {
        private readonly ApplicationDbContext data;

        public TestService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public bool ChangeVisibility(int id)
        {
            var test = data.Tests.FirstOrDefault(x => x.Id == id);

            if (test == null)
            {
                return false;
            }

            test.IsActive = !test.IsActive;

            data.Tests.Update(test);

            data.SaveChanges();

            return true;
        }

        public bool Create(string name, DateTime startDate, DateTime endDate, TimeSpan duration, string authorId)
        {
            if (startDate.CompareTo(endDate) != -1)
            {
                return false;
            }

            var test = new Test
            {
                Name = name,
                StartDate = startDate,
                EndDate = endDate,
                Duration = duration,
                IsActive = false,
                AuthorId = authorId
            };

            data.Tests.Add(test);
            data.SaveChanges();

            return true;
        }

        public TestServiceDetailsModel Details(int id)
        {
            var test = data.Tests
                .Select(x => new TestServiceDetailsModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Duration = x.Duration,
                    IsActive = x.IsActive,
                    AuthorId = x.AuthorId,
                    Author = x.Author,
                    Questions = x.Questions.Select(q => new QuestionServiceModel
                    {
                        Id = q.Id,
                        Content = q.Content
                    })
                    .ToList()
                })
                .FirstOrDefault(x => x.Id == id);


            return test;
        }

        public bool Edit(int id, string name, DateTime startDate, DateTime endDate, TimeSpan duration)
        {
            var test = data.Tests.FirstOrDefault(x => x.Id == id);

            if (test == null || startDate.CompareTo(endDate) != -1)
            {
                return false;
            }

            test.Name = name;
            test.StartDate = startDate;
            test.EndDate = endDate;
            test.Duration = duration;

            data.Update(test);

            data.SaveChanges();

            return true;
        }

        public IEnumerable<TestServiceModel> GetAllTests()
        {
            var allTests = data.Tests.Select(x => new TestServiceModel
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive
            })
              .ToList();

            return allTests;
        }

        public IEnumerable<ActiveTestsListingServiceModel> GetAllActiveTests(string userId)
        {
            var completedTestsIds = this.CompletedTests(userId).Select(x => x.Id).ToList();

            var dateNow = DateTime.Now;

            var allTests = data.Tests.Where(x => x.IsActive == true && x.Questions.Count() > 0).Select(x => new ActiveTestsListingServiceModel
            {
                Id = x.Id,
                Name = x.Name,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Duration = x.Duration,
                IsCompleted = completedTestsIds.Contains(x.Id),
                IsDateInValidRange = dateNow.CompareTo(x.EndDate) != 1 && dateNow.CompareTo(x.StartDate) != -1
            })
              .ToList();

            return allTests;
        }

        public int SubmitUserAnswers(string input, string userId)
        {
            var test = JsonConvert.DeserializeObject<SubmitedAnswersJsonServiceModel>(input);
            var testId = test.TestId;
            var points = 0;
            foreach (var item in test.Answers)
            {
                var questionId = item.QuestionId;
                var answerId = item.AnswerId;
                var text = item.TextAnswer;

                var userAnswer = new UsersAnswers
                {
                    AnswerId = answerId == 0 ? null : answerId,
                    QuestionId = questionId,
                    UserId = userId,
                    AnswerText = text
                };

                data.UsersAnswers.Add(userAnswer);
                points += data.Questions.Where(x => x.Answers.Any(x => x.Id == answerId && x.IsCorrect.Value == true && x.Question.Test.Id == testId)).Select(x => x.Points).FirstOrDefault();
            }

            data.SaveChanges();

            var results = new Result
            {
                UserId = userId,
                TestId = testId,
                Points = points
            };

            data.Results.Add(results);
            data.SaveChanges();
            return points;
        }

        public IEnumerable<TestServiceModel> CompletedTests(string userId)
        {
            var userTests = this.data.Results
                .Where(x => x.UserId == userId)
                .Select(x => new TestServiceModel
                {
                    Id = x.TestId,
                    Name = data.Tests.Where(i => i.Id == x.TestId).FirstOrDefault().Name,
                    Points = data.Results.Where(i => i.TestId == x.TestId && x.UserId == userId).FirstOrDefault() == null ? 0 : data.Results.Where(i => i.TestId == x.TestId && x.UserId == userId).FirstOrDefault().Points,
                    Grade = data.Results.Where(i => i.TestId == x.TestId && x.UserId == userId).FirstOrDefault() == null ? null : data.Results.Where(i => i.TestId == x.TestId && x.UserId == userId).FirstOrDefault().Grade

                }).Distinct()
                .ToList();


            return userTests;

        }

        public ICollection<UnvaluatedTestsServiceModel> GetAllUnvaluatedTests()
        {
            var tests = this.data.Results.Where(x => x.Grade == null)
                .Select(x => new UnvaluatedTestsServiceModel
                {
                    ResultId = x.Id,
                    Id = x.TestId,
                    Name = data.Tests.Where(i => i.Id == x.TestId).FirstOrDefault().Name,
                    UserId = x.UserId,
                    Username = data.Users.Where(u => u.Id == x.UserId).FirstOrDefault().UserName,
                    PointsFromClosedQuestions = x.Points,
                    IsTestEvaluated = x.IsChecked

                }).ToList();

            return tests;
        }

        public ICollection<OpenQuestionAnswerServiceModel> GetOpenedAnswersForSolvedTest(string userId, int testId, int resultId)
        {
            var answers = data.UsersAnswers
                    .Where(ua => ua.UserId == userId && ua.Question.QuestionType.TypeName == "Opened" && ua.Question.TestId == testId)
                    .Select(ua => new OpenQuestionAnswerServiceModel
                    {
                        ResultId = resultId,
                        QuestionContent = ua.Question.Content,
                        QuestionId = ua.QuestionId,
                        AnswerText = ua.AnswerText,
                        MaxPoints = ua.Question.Points,
                        PointsForAnswer = 0
                    }).ToList();

            return answers;
        }

        public void AddPointsToResult(string json)
        {
            var parameters = JsonConvert.DeserializeObject<AddPointsServiceModel>(json);
            var result = this.data.Results.Where(x => x.Id == parameters.ResultId).FirstOrDefault();
            result.Points += parameters.Points;
            result.IsChecked = true;
            this.data.Results.Update(result);
            this.data.SaveChanges();
        }

        public void AddGradeToResult(string json)
        {
            var parameters = JsonConvert.DeserializeObject<AddGradeServiceModel>(json);
            var result = this.data.Results.Where(x => x.Id == parameters.ResultId).FirstOrDefault();
            result.Grade = parameters.Grade;
            this.data.Results.Update(result);
            this.data.SaveChanges();
        }

        public ICollection<EvaluatedTestsServiceModel> GetAllEvaluatedTests()
        {
            var tests = this.data.Results.Where(x => x.Grade != null)
               .Select(x => new EvaluatedTestsServiceModel
               {
                  Name= data.Tests.Where(i => i.Id == x.TestId).FirstOrDefault().Name,
                  Username= data.Users.Where(u => u.Id == x.UserId).FirstOrDefault().UserName,
                  Grade=x.Grade.Value,
                  Points=x.Points
               }).ToList();

            return tests;

        }

        public ICollection<TestCheckAnswersServiceModel> CheckTest(int testId, string userId)
        {
            var test = this.data.UsersAnswers
                .Where(x => x.Question.TestId == testId && x.Question.QuestionType.TypeName == "Closed")
                .Select(x => new TestCheckAnswersServiceModel
                {
                    QuestionId = x.QuestionId,
                    QuestionContent=x.Question.Content,
                    CorrectAnswerId = x.Question.Answers.FirstOrDefault(x => x.IsCorrect.Value == true).Id,
                    CorrectAnswerContent= x.Question.Answers.FirstOrDefault(x => x.IsCorrect.Value == true).Content,
                    MyAnswerId = x.AnswerId.Value,
                    MyAnswerContent=x.Answer.Content

                })
                .ToList();

            return test;
        }
    }
}
