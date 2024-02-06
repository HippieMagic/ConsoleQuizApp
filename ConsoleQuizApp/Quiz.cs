using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleQuizApp
{
    internal class Quiz
    {
        private readonly List<QuizQuestion> _questions;
        //private int _currentQuestionIndex = 0;

        public Quiz(string filePath)
        {
            _questions = LoadQuestionsFromFile(filePath);
        }

        private List<QuizQuestion> LoadQuestionsFromFile(string filePath)
        {
            var questions = new List<QuizQuestion>();
            var lines = File.ReadAllLines(filePath);

            QuizQuestion currentQuestion = null;
            var isReadingQuestion = false;
            var isReadingAnswers = false;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("*"))
                    continue; // Skip comments and blank lines

                if (line.StartsWith("@Q"))
                {
                    isReadingQuestion = true;
                    currentQuestion = new QuizQuestion
                    {
                        Answers = new List<string>()
                    };
                    continue;
                }

                if (line.StartsWith("@A"))
                {
                    isReadingQuestion = false;
                    isReadingAnswers = true;
                    continue;
                }

                if (line.StartsWith("@E"))
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
                    if (int.TryParse(line, out var correctAnswer))
                    {
                        if (currentQuestion != null)
                            currentQuestion.CorrectAnswerIndex = correctAnswer - 1; // Assuming answers are 1-indexed
                    }
                    else
                    {
                        currentQuestion?.Answers.Add(line);
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

                for (var i = 0; i < question.Answers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {question.Answers[i]}");
                }

                Console.WriteLine("Enter the correct answer number:");
                var userAnswer = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine(userAnswer - 1 == question.CorrectAnswerIndex ? "Correct!" : "Incorrect.");
            }
        }
    }
}
