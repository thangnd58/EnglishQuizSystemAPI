using System;
using System.Collections.Generic;

namespace EnglishQuizSystem.Models
{
    public partial class Quiz
    {
        public Quiz()
        {
            Questions = new HashSet<Question>();
            UserQuizzes = new HashSet<UserQuiz>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CodeActive { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<UserQuiz> UserQuizzes { get; set; }
    }
}
