using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            //Menu();

            while (!gameOver)
            {
                Console.WriteLine("Игра началась!\nЯ загадал слово, отгадай его буква за буквой");
                Console.WriteLine("Слово:" + randomWord);
                char playerInput = Console.ReadKey().KeyChar;

                if (playerInput == '0')
                {
                    Console.WriteLine("Конец игры!");
                    gameOver = false;
                    Menu();
                }
                else if (playerInput == '1')
                {
                    gameOver = true;
                    Console.WriteLine("Играем заново!");
                    StartGame();
                }

                if (word.Contains(playerInput) && !guessedLetters.Contains(playerInput))
                {
                    Console.WriteLine("\nВерно!\n");
                }
                else
                {
                    Console.WriteLine("\nНе верно!\n");
                    errorCount++;
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

                if (errorCount > 32)
                {
                    Console.WriteLine("Вы проиграли!");
                    gameOver = true;
                    //break;
                    StartGame();
                }

                gameOver = wordComplete;

                if (wordComplete)
                {
                    Console.WriteLine("Вы выиграли!");
                    StartGame();
                }
                Console.WriteLine("Счетчик ошибок: " + errorCount);
            }
        }
        public static void Menu()
        {
            Console.WriteLine("1 - играть или играть заново.\n0 - выход");
            //int input = Convert.ToInt32(Console.ReadLine());
            int input;

            try
            {
                int.TryParse(Console.ReadLine(), out input);
            }
            catch (Exception e)
            {
                Console.WriteLine("Co");
                throw;
            }

            switch (input)
            {
                case (int)MenuChoice.Play:
                    Console.WriteLine("Играем!");
                    break;
                case (int)MenuChoice.Exit:
                    Console.WriteLine("Не играем");
                    break;
                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }
        }
    }
}