namespace QuizSystemWeb.Areas.Admin.Models.Answer
{
    using QuizSystemWeb.Data.Entities;
    using QuizSystemWeb.Services.Answers.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static QuizSystemWeb.Data.DataConstants.Answer;

    public class AnswerFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Correct")]
        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }

        public ICollection<AnswerSignificanceServiceModel> ChoicesList { get; set; }
    }
}
