namespace QuizSystemWeb.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using QuizSystemWeb.Areas.Admin.Models.Answer;
    using QuizSystemWeb.Services.Answers;

    public class AnswersController : AdministratorController
    {
        private readonly IAnswerService answerService;

        public AnswersController(IAnswerService answerService)
        {
            this.answerService = answerService;
        }

        public IActionResult Create(int id)
        {
            var choicesList = answerService.GetAnswerSignificances();
            return View(new AnswerFormViewModel()
            { QuestionId=id , 
              ChoicesList=answerService.GetAnswerSignificances()
            });
        }

        [HttpPost]
        public IActionResult Create(AnswerFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            

            if (!(answerService.Create(model.QuestionId, model.Content, model.IsCorrect)))
            {
                this.TempData[WebConstants.GlobalErrorMessageKey] = "Can not create correct answer because correct answer already exists!";
            }
            else
            {
                this.TempData[WebConstants.GlobalMessageKey] = "Answer was succesfully added to question!";
            }
           
            return RedirectToAction("Details", "Questions", new { Id = model.QuestionId });
        }

        public IActionResult Edit(int id)
        {
            var answer = answerService.GetById(id);
            var model = new AnswerFormViewModel
            {
                Id = answer.Id,
                Content = answer.Content,
                IsCorrect = answer.IsCorrect.Value,
                QuestionId = answer.QuestionId,
                ChoicesList = answerService.GetAnswerSignificances()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(AnswerFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var answerId = model.Id;
            var content = model.Content;
            var isCorrect = model.IsCorrect;

            if (!(answerService.Edit(answerId, content, isCorrect)))
            {
                this.TempData[WebConstants.GlobalErrorMessageKey] = "Can not edit answer to correct answer because correct answer already exists!";
            }
            else
            {
                this.TempData[WebConstants.GlobalMessageKey] = "Answer was succesfully edited";
            }
            ;
            return RedirectToAction("Details", "Questions", new { id = model.QuestionId });
        }

    }
}
