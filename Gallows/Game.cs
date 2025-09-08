namespace Gallows;

public static class Game
{
    // Dictionary path
    private const string DictionaryFileName = "Dictionary.txt";
    private static readonly string path = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", DictionaryFileName);
    private static readonly string[] dictionary = File.ReadAllLines(path);
    private const int maxErrorCount = 10;

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
                Console.Clear();
            }
            else if (!Char.IsLetter(playerInput))
            {
                Console.Clear();
                Console.WriteLine("\nНе вводите цифры!\n");
            }
            else
            {
                Console.Clear();
                DrawGallows.Draw(errorCount);
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


            if (guessedLetters.Contains(playerInput))
            {
                Console.Clear();
                Console.WriteLine($"\nВы уже вводили букву '{playerInput}'!\nПродолжите со следующей буквы\n");
                continue;
            }

            guessedLetters.Add(playerInput);
            foreach (char c in word)
            {
                Console.Write(guessedLetters.Contains(c) ? c : '_');
            }
            Console.WriteLine("\n");

            foreach (char c in word)
            {
                //if (!guessedLetters.Contains(c))
                //{
                //    wordComplete = false;
                //}
                //else
                //{
                //    wordComplete = true;
                //}
                wordComplete = guessedLetters.Contains(c) ? true : false;

            }

            if (wordComplete)
            {
                DrawGallows.Draw(errorCount);
                Console.WriteLine("Вы выиграли!\nНажмите любую кнопку чтобы продолжить");
                Console.ReadKey();
                Menu.Start();
                StartGame();
            }

            gameOver = wordComplete;
        }
    }


}