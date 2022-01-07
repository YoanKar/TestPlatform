namespace QuizSystemWeb.Areas.Admin.Models.Question
{
    using QuizSystemWeb.Services.Questions.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using static Data.DataConstants.Question;

    public class QuestionFormModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        public int Points { get; set; }

        public int QuestionType { get; set; }

        public int TestId { get; set; }

        public ICollection<QuestionTypesServiceModel> TypesList { get; set; }
    }
}
