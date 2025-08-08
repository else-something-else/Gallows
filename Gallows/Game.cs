using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

namespace Gallows
{
    public static class Game
    {
        enum MenuChoice
        {
            Exit = 0,
            Play = 1
        }

        public static void StartGame()
        {
            bool gameOver = false;
            int errorCount = 0;

            // Path to dictionary
            const string fileName = "Dictionary.txt";
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            string[] dictionary = File.ReadAllLines(path);
            var rnd = new Random();
            string randomWord = dictionary[rnd.Next(dictionary.Length)];

            string word = randomWord;
            List<char> guessedLetters = new List<char>();

            Console.Clear();
            Console.WriteLine("Игра началась!\nЯ загадал слово, отгадай его буква за буквой, у тебя 10 попыток");
            Console.WriteLine("Слово: " + randomWord);

            while (!gameOver)
            {
                char playerInput = Console.ReadKey().KeyChar;

                if (guessedLetters.Contains(playerInput))
                {
                    continue;
                }

                if (word.Contains(playerInput))
                {
                    //Console.WriteLine("\nВерно!\n");
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    //Console.WriteLine("\nНе верно!\n");
                    DrawGallows(errorCount);
                    errorCount++;
                }

                if (errorCount > 10)
                {
                    Console.WriteLine("Играем заново?");
                    gameOver = true;
                    Menu();
                    StartGame();
                }

                guessedLetters.Add(playerInput);
                foreach (char c in word)
                {
                    if (guessedLetters.Contains(c))
                    {
                        Console.Write(c);
                    }
                    else
                    {
                        Console.Write("_");
                    }
                }
                Console.WriteLine("\n");

                bool wordComplete = true;

                foreach (char c in word)
                {
                    if (!guessedLetters.Contains(c))
                    {
                        wordComplete = false;
                    }
                }

                if (wordComplete)
                {
                    DrawGallows(errorCount);
                    Console.WriteLine("Вы выиграли!\nНажмите любую кнопку чтобы продолжить");
                    Console.ReadKey();
                    Menu();
                    StartGame();
                }

                gameOver = wordComplete;
                //Console.WriteLine("Счетчик ошибок: " + errorCount);
            }
        }
        public static void Menu()
        {
            int input;
            bool menuExit = false;

            Console.Clear();
            Console.WriteLine("1 - играть или играть заново.\n0 - выход");

            while (!menuExit)
            {
                if (!int.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("Некорректный ввод! Повторите попытку.");
                    Console.WriteLine("1 - играть или играть заново.\n0 - выход");
                    continue;
                }
                else
                {
                    switch ((MenuChoice)input)
                    {
                        case MenuChoice.Play:
                            menuExit = true;
                            break;
                        case MenuChoice.Exit:
                            Console.WriteLine("Не играем!");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Неверный выбор!");
                            break;
                    }
                }
            }
        }
        public static void DrawGallows(int errorCount)
        {
            string[] gallows = new string[]
            {
                        " _________     ",
                        " |       |     ",
                        $" |       {(errorCount >= 1 ? "O" : " ")}",
                        $" |      {(errorCount >= 3 ? "/" : " ")}{(errorCount >= 2 ? "|" : " ")}{(errorCount >= 4 ? "\\" : " ")}",
                        $" |      {(errorCount >= 5 ? "/" : " ")} {(errorCount >= 6 ? "\\" : " ")}",
                        " |             ",
                        "_|_            "
            };

            foreach(string line in gallows)
            {
                Console.WriteLine(line);
            }
        }
    }
}