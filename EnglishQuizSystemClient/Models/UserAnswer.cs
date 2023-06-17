namespace EnglishQuizSystemClient.Models
{
    public class UserAnswer
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int QuizId { get; set; }
    }
}
