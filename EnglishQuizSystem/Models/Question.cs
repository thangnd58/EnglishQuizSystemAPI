using System;
using System.Collections.Generic;

namespace EnglishQuizSystem.Models
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            UserAnswers = new HashSet<UserAnswer>();
        }

        public int Id { get; set; }
        public int? QuizId { get; set; }
        public string? Text { get; set; }
        public bool? Active { get; set; }
        public bool? Type { get; set; }
        public int? Difficulty { get; set; }

        public virtual Quiz? Quiz { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
