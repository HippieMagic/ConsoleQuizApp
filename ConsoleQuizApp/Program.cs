using System;

namespace ConsoleQuizApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var quiz = new Quiz(@"C:\Users\nick_\Downloads\sample.q");
            quiz.Start();
        }
    }
}
