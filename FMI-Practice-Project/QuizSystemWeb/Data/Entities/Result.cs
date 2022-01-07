namespace QuizSystemWeb.Data.Entities
{
    public class Result
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TestId { get; set; }
        public int Points { get; set; }
        public int? Grade { get; set; }
        public bool IsChecked { get; set; }
    }
}
