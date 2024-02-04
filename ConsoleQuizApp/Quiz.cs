using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleQuizApp
{
    internal class Quiz
    {
        private List<QuizQuestion> _questions;
        private int _currentQuestionIndex = 0;

        public Quiz(string filePath)
        {
            _questions = LoadQuestionsFromFile(filePath);
        }

        private List<QuizQuestion> LoadQuestionsFromFile(string filePath)
        {
            // Implement the file reading and parsing logic here
            // Refer to the previous example for parsing logic
            // ...
            var questions = new List<QuizQuestion>();
            var lines = File.ReadAllLines(filePath);

            QuizQuestion currentQuestion = null;
            bool isReadingQuestion = false;
            bool isReadingAnswers = false;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("*"))
                    continue; // Skip comments and blank lines

                if (line.StartsWith("@QUESTION"))
                {
                    isReadingQuestion = true;
                    currentQuestion = new QuizQuestion();
                    currentQuestion.Answers = new List<string>();
                    continue;
                }

                if (line.StartsWith("@ANSWERS"))
                {
                    isReadingQuestion = false;
                    isReadingAnswers = true;
                    continue;
                }

                if (line.StartsWith("@END"))
                {
                    isReadingAnswers = false;
                    questions.Add(currentQuestion);
                    continue;
                }

                if (isReadingQuestion)
                {
                    currentQuestion.QuestionText += line + " ";
                }
                else if (isReadingAnswers)
                {
                    if (int.TryParse(line, out int correctAnswer))
                    {
                        currentQuestion.CorrectAnswerIndex = correctAnswer - 1; // Assuming answers are 1-indexed
                    }
                    else
                    {
                        currentQuestion.Answers.Add(line);
                    }
                }
            }

            // Randomize the questions
            Random rnd = new Random();
            questions = questions.OrderBy(q => rnd.Next()).ToList();

            return questions;
        }

        public void Start()
        {
            foreach (var question in _questions)
            {
                Console.WriteLine(question.QuestionText);
                foreach (var answer in question.Answers)
                {
                    Console.WriteLine(answer);
                }

                Console.WriteLine("Enter the correct answer number:");
                var userAnswer = Convert.ToInt32(Console.ReadLine());

                if (userAnswer == question.CorrectAnswerIndex + 1)
                {
                    Console.WriteLine("Correct!");
                }
                else
                {
                    Console.WriteLine("Incorrect.");
                }
            }
        }
    }
}
