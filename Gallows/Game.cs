using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Channels;

namespace Gallows;

public static class Game
{
    // Dictionary path
    private const string DictionaryfileName = "Dictionary.txt";
    private static readonly string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DictionaryfileName);
    private static readonly string[] dictionary = File.ReadAllLines(path);
    private const int maxErrorCount = 10;

    private static readonly string[][] gallows =
    {
        new string[]
        {
            "  _______",
            "  |     |",
            "  |",
            "  |",
            "  |",
            "  |",
            "__|__"
        },
        new string[]
        {
            "  _______",
            "  |     |",
            "  |     O",
            "  |",
            "  |",
            "  |",
            "__|__"
        },
        new string[]
        {
            "  _______",
            "  |     |",
            "  |     O",
            "  |     |",
            "  |     |",
            "  |",
            "__|__"
        },
        new string[]
        {
            "  _______",
            "  |     |",
            "  |     O",
            "  |    /|",
            "  |     |",
            "  |",
            "__|__"
        },
        new string[]
        {
            "  _______",
            "  |     |",
            "  |     O",
            "  |    /|\\",
            "  |     |",
            "  |",
            "__|__"
        },
        new string[]
        {
            "  _______",
            "  |     |",
            "  |     O",
            "  |    /|\\",
            "  |     |",
            "  |    /",
            "__|__"
        },
        new string[]
        {
            "  _______",
            "  |     |",
            "  |     O",
            "  |    /|\\",
            "  |     |",
            "  |    / \\",
            "__|__"
        }
    };

    public static void StartGame()
    {
        int errorCount = 0;
        bool gameOver = false;
        bool wordComplete = true;
        Random rnd = new Random();
        string randomWord = dictionary[rnd.Next(dictionary.Length)];
        string word = randomWord;
        List<char> guessedLetters = new List<char>();

        Console.Clear();
        Console.WriteLine("Игра началась!\nЯ загадал слово, отгадай его буква за буквой, у тебя 10 попыток");
        Console.WriteLine("Слово: " + randomWord);

        while (!gameOver)
        {
            char playerInput = Console.ReadKey().KeyChar;

            if (word.Contains(playerInput) && Char.IsLetter(playerInput))
            {
                Console.Clear() ;
            }
            else if(!Char.IsLetter(playerInput))
            {
                Console.Clear();
                Console.WriteLine("\nНе вводите цифры!\n");
            }
            else
            {
                Console.Clear();
                DrawGallows(errorCount);
                errorCount++;
                Console.WriteLine($"errorCount: {errorCount}");
            }

            if (errorCount > maxErrorCount)
            {
                Console.WriteLine("Играем заново?");
                gameOver = true;
                Menu.Start();
                StartGame();
            }

            guessedLetters.Add(playerInput);
            foreach (char c in word)
            {
                if (guessedLetters.Contains(c))
                {
                    Console.Write(c);
                }
                else if (word.Contains(c))
                    break;
                else
                {
                    Console.Write("_");
                }
            }
            Console.WriteLine("\n");



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
                Menu.Start();
                StartGame();
            }

            gameOver = wordComplete;
            //Console.WriteLine("Счетчик ошибок: " + errorCount);
        }
    }

    public static void DrawGallows(int errorCount)
    {
        int index = Math.Min(errorCount, gallows.Length - 1);
        foreach (var line in gallows[index])
        {
            Console.WriteLine(line);
        }
    }
}