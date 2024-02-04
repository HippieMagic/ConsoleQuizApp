using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleQuizApp
{
    internal class QuizQuestion
    {
        public string QuestionText { get; set; }
        public List<string> Answers { get; set; }
        public int CorrectAnswerIndex { get; set; }
    }
}
