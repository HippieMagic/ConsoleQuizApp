# Console Quiz Application

## Overview

The Console Quiz Application is a simple, interactive quiz program written in C#. It allows users to take quizzes based on questions loaded from a text file. The application reads a file containing quiz questions and answers, then presents these to the user in a randomized order. After answering a set number of questions, the user is presented with their score.

## Features

- Load quiz questions and answers from a text file.
- Interactive quiz experience in the console.
- Randomized question order.
- Score summary at the end of the quiz.

## Getting Started

### Prerequisites

- .NET Framework installed on your machine.

### Installation

1. Clone the repository to your local machine.
2. Navigate to the cloned directory.

### Running the Application

1. Compile the application using your preferred C# compiler or development environment.
2. Run the compiled executable.
3. Follow the on-screen instructions to input the location of your quiz file and the number of questions you'd like to answer.

## How to Use

- When prompted, enter the file path of your quiz file.
- The quiz file should be formatted properly (details provided in the 'Quiz File Format' section).
- Answer the questions by entering the number corresponding to your chosen answer.
- At the end of the quiz, your score will be displayed.

## Quiz File Format

The quiz file should be a text file with the following format:

- Questions are marked with `@Q` at the beginning.
- Answers are marked with `@A`, followed by the answer choices, one per line.
- The correct answer is marked with its corresponding number after `@A`.
- The end of each question block is marked with `@E`.

## License

This project is licensed under the [MIT License](LICENSE.md).
