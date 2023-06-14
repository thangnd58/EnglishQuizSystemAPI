using System;
using System.Collections.Generic;

namespace EnglishQuizSystem.Models
{
    public partial class UserAnswer
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int QuizId { get; set; }

        public virtual Answer Answer { get; set; } = null!;
        public virtual Question Question { get; set; } = null!;
        public virtual Quiz Quiz { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
