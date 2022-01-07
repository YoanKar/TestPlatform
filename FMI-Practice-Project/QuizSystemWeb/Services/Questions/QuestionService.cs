namespace QuizSystemWeb.Services.Questions
{
    using QuizSystemWeb.Data;
    using QuizSystemWeb.Data.Entities;
    using QuizSystemWeb.Services.Answers;
    using QuizSystemWeb.Services.Answers.Models;
    using QuizSystemWeb.Services.Questions.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDbContext data;
        private readonly IAnswerService answerService;

        public QuestionService(ApplicationDbContext data, IAnswerService answerService)
        {
            this.data = data;
            this.answerService = answerService;
        }

        public bool Create(string content, int points, int questionTypeId, int testId)
        {
            var question = new Question
            {
                Content = content,
                Points = points,
                QuestionTypeId = questionTypeId,
                TestId = testId
            };

            this.data.Questions.Add(question);

            this.data.SaveChanges();

            return true;

        }

        public bool Edit(int questionId, string content, int questionType,int points)
        {
            var question = data.Questions.Where(x => x.Id == questionId).FirstOrDefault();

            var questionTypeId = this.data.QuestionsTypes.FirstOrDefault(x => x.TypeName == "Opened").Id;

            if(questionType == questionTypeId)
            {
                answerService.Delete(questionId);
            }

            var IsQuestionTypeValid = data.QuestionsTypes
                .Select(x => x.Id)
                .ToList()
                .Contains(questionType);

            if (!IsQuestionTypeValid)
            {
                return false;
            }


            question.Content = content;
            question.QuestionTypeId = questionType;
            question.Points = points;



            data.Update(question);
            data.SaveChanges();

            return true;


        }

        public ICollection<QuestionServiceModel> GetAllQuestions(int testId)
        {
            var questions = data.Questions
                .Where(x => x.TestId == testId)
                .Select(x => new QuestionServiceModel
                {
                    Id = x.Id,
                    Content = x.Content,
                    
                })
                .ToList();

            return questions;
        }

        public QuestionsListingServiceModel GetAllTestQuestions(int testId)
        {
            var questions = data.Questions
                .Where(x => x.TestId == testId)
                .Select(x => new QuestionDetailsServiceModel
                {
                    Id = x.Id,
                    Content = x.Content,
                    Answers = x.Answers.Select(a => new AnswerDetailsServiceModel
                    {
                        Id = a.Id,
                        Content = a.Content,
                        IsCorrect = new AnswerSignificanceServiceModel
                        {
                            Name = a.IsCorrect.Name,
                            Value = a.IsCorrect.Value
                        },
                        QuestionId = a.QuestionId
                    })
                    .ToList(),
                    Points = x.Points,
                    QuestionType = x.QuestionType.TypeName,
                    QuestionTypeId = x.QuestionTypeId,
                    TestId = x.TestId,
                    Test = x.Test
                })
                .ToList();

            var questionsList = new QuestionsListingServiceModel
            {
                DeadLine = DateTime.Now + data.Tests.Where(x => x.Id == testId).FirstOrDefault().Duration,
                QuestionsList = questions
            };

            return questionsList;
        }

        public QuestionDetailsServiceModel GetQuestionById(int questionId)
        {
            var question = data.Questions
                .Where(x => x.Id == questionId)
                .Select(x => new QuestionDetailsServiceModel
                {
                    Id = x.Id,
                    Content = x.Content,
                    Points = x.Points,
                    QuestionType = x.QuestionType.TypeName,
                    Answers = x.Answers.Select(a => new AnswerDetailsServiceModel{
                        Id = a.Id,
                        Content = a.Content,
                        IsCorrect = new AnswerSignificanceServiceModel() 
                        {
                            Name=a.IsCorrect.Name,
                            Value=a.IsCorrect.Value,
                        }
                    })
                    .ToList(),
                    TestId = x.TestId
                })
                .FirstOrDefault();

            return question;
        }

        public ICollection<QuestionTypesServiceModel> GetQuestionTypes()
        => this.data.QuestionsTypes.Select(x => new QuestionTypesServiceModel
        {
            Id = x.Id,
            Name = x.TypeName
        }).ToList();
            
    }
}
