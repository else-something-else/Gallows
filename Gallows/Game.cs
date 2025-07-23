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
        public static void StartGame()
        {
            bool gameOver = false;

            Console.WriteLine("1 - играть или играть заново.\n0 - выход");
            int input = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(input);

            // Menu
            if (input != 1)
            {
                gameOver = true;
                Console.WriteLine("Цонец игры!");
            }
            else
            {
                int errorCount = 0;

                Console.WriteLine("Игра началась!\nЯ загадал слово, отгадай его буква за буквой");

                // Path to dictionary
                string fileName = "Dictionary.txt";
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                string[] dictionary = File.ReadAllLines(path);
                var rnd = new Random();
                string randomWord = dictionary[rnd.Next(dictionary.Length)];

                Console.WriteLine("Слово:" + randomWord);

                string word = randomWord;
                List<char> guessedLetters = new List<char>();

                while (!gameOver)
                {
                    char playerInput = Console.ReadKey().KeyChar;

                    if (playerInput == '0')
                    {
                        Console.WriteLine("Конец игры!");
                        gameOver = false;
                        break;
                    }
                    else if (playerInput == '1')
                    {
                        gameOver = true;
                        Console.WriteLine("Играем заново!");
                        Game.StartGame();
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
                        Game.StartGame();
                    }
                    
                    gameOver = wordComplete;

                    if (wordComplete)
                    {
                        Console.WriteLine("Вы выиграли!");
                        Game.StartGame();
                    }
                    Console.WriteLine("Счетчик ошибок: " + errorCount);
                }
            }
        }
    }
}