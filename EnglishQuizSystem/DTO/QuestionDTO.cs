namespace EnglishQuizSystem.DTO
{
	public class QuestionDTO
	{
		public int Id { get; set; }
		public int? QuizId { get; set; }
		public string? Text { get; set; }
		public bool? Active { get; set; }
		public bool? Type { get; set; }
		public int? Difficulty { get; set; }
	}
}
