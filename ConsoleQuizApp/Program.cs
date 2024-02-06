using System;

namespace ConsoleQuizApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter your file location: ");
            var userAnswer = Convert.ToString(Console.ReadLine());
            var quiz = new Quiz(userAnswer);
            quiz.Start();
        }
    }
}
