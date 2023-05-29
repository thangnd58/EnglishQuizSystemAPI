using System;
using System.Collections.Generic;

namespace EnglishQuizSystem.Models
{
    public partial class User
    {
        public User()
        {
            UserAnswers = new HashSet<UserAnswer>();
            UserQuizzes = new HashSet<UserQuiz>();
        }

        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
        public virtual ICollection<UserQuiz> UserQuizzes { get; set; }
    }
}
