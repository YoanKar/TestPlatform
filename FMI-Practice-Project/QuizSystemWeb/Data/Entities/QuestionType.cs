namespace QuizSystemWeb.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.QuestionType;

    public class QuestionType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(TypeMaxLength)]
        public string TypeName { get; set; }
    }
}
