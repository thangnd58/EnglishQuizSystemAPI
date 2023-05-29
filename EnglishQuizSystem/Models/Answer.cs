﻿using System;
using System.Collections.Generic;

namespace EnglishQuizSystem.Models
{
    public partial class Answer
    {
        public Answer()
        {
            UserAnswers = new HashSet<UserAnswer>();
        }

        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public string? Text { get; set; }
        public bool? IsCorrect { get; set; }

        public virtual Question? Question { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
    }
}
