namespace QuizSystemWeb.Services.Tests.Models
{
    using QuizSystemWeb.Data.Entities;
    using QuizSystemWeb.Services.Questions.Models;
    using System;
    using System.Collections.Generic;

    public class TestServiceDetailsModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TimeSpan Duration { get; set; }

        public bool IsActive { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public virtual ICollection<QuestionServiceModel> Questions { get; set; }
     
    }
}
