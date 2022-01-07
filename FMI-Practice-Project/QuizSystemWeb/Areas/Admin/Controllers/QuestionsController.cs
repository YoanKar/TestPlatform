namespace QuizSystemWeb.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using QuizSystemWeb.Areas.Admin.Models.Question;
    using QuizSystemWeb.Services.Questions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class QuestionsController:AdministratorController
    {
        private readonly IQuestionService questionService;

        public QuestionsController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        public IActionResult Create(int id)
        {
            var questionTypes = this.questionService.GetQuestionTypes();

            return View(new QuestionFormModel() { TestId=id , TypesList=questionTypes});
        }

        [HttpPost]
        public IActionResult Create(QuestionFormModel model)
        {
            questionService.Create(model.Content, model.Points, model.QuestionType, model.TestId);

            this.TempData[WebConstants.GlobalMessageKey] = "Question was succesfully added to test!";

            return RedirectToAction("Details", "Tests",new { Id=model.TestId });
        }

        public IActionResult Details(int id)
        {
            var model = questionService.GetQuestionById(id);

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var question = questionService.GetQuestionById(id);

            var questionDto = new QuestionFormModel
            {
                Id = id,
                Content = question.Content,
                Points = question.Points,
                TypesList= this.questionService.GetQuestionTypes()

        };


            return View(questionDto);
        }

        [HttpPost]
        public IActionResult Edit(QuestionFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!(questionService.Edit(model.Id, model.Content, model.QuestionType,model.Points)))
            {
                this.TempData[WebConstants.GlobalErrorMessageKey] = "Can not edit question because question type is invalid!";
            }
            else
            {
                this.TempData[WebConstants.GlobalMessageKey] = "Question was succesfully edited!";
            }
            ;
            return RedirectToAction("Details", "Questions", new { id = model.Id });


        }

    }
}
