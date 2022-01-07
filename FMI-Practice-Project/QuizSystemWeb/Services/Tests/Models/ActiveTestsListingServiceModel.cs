using System;

namespace QuizSystemWeb.Services.Tests.Models
{
    public class ActiveTestsListingServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TimeSpan Duration { get; set; }

        public bool IsCompleted { get; set; }

        public bool IsDateInValidRange { get; set; }
    }
}
