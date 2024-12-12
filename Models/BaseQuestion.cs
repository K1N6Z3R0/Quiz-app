using System.Collections.Generic;

namespace QuizApp.Models
{
    public abstract class BaseQuestion
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public int CorrectOptionIndex { get; set; }

        // A virtual or abstract method for displaying info
        public abstract void DisplayInfo();

    }
}
