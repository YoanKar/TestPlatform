namespace QuizSystemWeb.Data
{
    public class DataConstants
    {
        public class Answer
        {
            public const int ContentMinLength = 2;

            public const int ContentMaxLength = 250;

            public const int AnswerTextMaxLength = 500;

        }

        public class AnswerSignificance
        {
            public const int NameMaxLength = 3;

        }

        public class Question
        {
            public const int ContentMinLength = 10;

            public const int ContentMaxLength = 250;
        }

        public class QuestionType
        {
            public const int TypeMaxLength = 15;
        }

        public class Test
        {
            public const int NameMinLength = 10;

            public const int NameMaxLength = 100;
        }

        public class User
        {
            public const int FullNameMinLength = 10;

            public const int FullNameMaxLength = 50;
        }
    }
}
