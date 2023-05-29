using System;
using System.Collections.Generic;

namespace EnglishQuizSystem.Models
{
    public partial class UserQuiz
    {
        public int UserId { get; set; }
        public int QuizId { get; set; }
        public double? Score { get; set; }

        public virtual Quiz Quiz { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
