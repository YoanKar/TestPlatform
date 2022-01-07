using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizSystemWeb.Services.Tests.Models
{
    public class UnvaluatedTestsServiceModel
    {
        public int ResultId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        public int PointsFromClosedQuestions { get; set; }

        public bool IsTestEvaluated { get; set; }

        [Range(2, 6)]
        public int? Grade { get; set; }
    }
}
