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

        public Quiz(string filePath)
        {
            _questions = LoadQuestionsFromFile(filePath);
        }

        public Quiz()
        {

        }

        public void StartNewQuiz()
        {
            Console.WriteLine("Please enter your file location: ");
            var userAnswer = Convert.ToString(Console.ReadLine());
            var quiz = new Quiz(userAnswer);

            Console.WriteLine("How many questions would you like to answer?");
            var numOfQuestions = Convert.ToInt32(Console.ReadLine());
            quiz.Start(numOfQuestions);
        }

        private List<QuizQuestion> LoadQuestionsFromFile(string filePath)
        {
            var questions = new List<QuizQuestion>();
            string[] lines = null;
            
            // make sure the file exists and weed out junk responses
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading file at: " + filePath);
                StartNewQuiz();
            }

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
            var rnd = new Random();
            questions = questions.OrderBy(q => rnd.Next()).ToList();

            return questions;
        }

        public void Start(int numOfQuestions)
        {
            var rnd = new Random();
            _questions = _questions.OrderBy(q => rnd.Next()).ToList();
            var askedQuestions = 0;
            var correctAnswers = 0;

            foreach (var question in _questions.Take(numOfQuestions))
            {
                Console.WriteLine(question.QuestionText);
                for (var i = 0; i < question.Answers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {question.Answers[i]}");
                }

                Console.WriteLine("Enter the number of the correct answer:");
                if (!int.TryParse(Console.ReadLine(), out var userAnswer) || userAnswer < 1 || userAnswer > question.Answers.Count)
                {
                    Console.WriteLine("Invalid input. Please enter a number from the list of answers.");
                    continue;
                }

                if (userAnswer - 1 == question.CorrectAnswerIndex)
                {
                    Console.WriteLine("Correct!");
                    correctAnswers++;
                }
                else
                {
                    Console.WriteLine("Incorrect.");
                }

                if (askedQuestions == numOfQuestions)
                {
                    break;
                }
                
                askedQuestions++;
            }

            // Display summary
            Console.WriteLine($"Quiz completed. Questions asked: {askedQuestions}, Correct answers: {correctAnswers}, Accuracy: {(double)correctAnswers / askedQuestions * 100}%");
        }

    }
}
