using System.Diagnostics;
using System.Text;

internal static class mathGame
{
    private static readonly Random Rng = new Random();

    static void Main()
    {
        List<string> saveStore = new List<string>();
        Console.WriteLine("New Game");
        Console.WriteLine("Saved Games");
        int score = 0;

        while (true)
        {
            Console.WriteLine("Press the 'N' key to start a new game or the 'S' key to view saved games");
            string? userInput = Console.ReadLine();

            if (string.Equals(userInput, "n", StringComparison.OrdinalIgnoreCase))
            {
                while (true)
                {
                    char operatorInput = ReadOperator();
                    var difficultyLevel = ReadDifficulty();
                    var round = PlayRound(difficultyLevel, operatorInput, score);
                    score = round.updatedScore;

                    Console.WriteLine($"{difficultyLevel.Name}: {round.question}");
                    Console.WriteLine($"Score: {score}");
                    Console.WriteLine($"Time taken: {round.elapsed.TotalSeconds:F2}s");
                    Console.WriteLine(round.correct);

                    saveStore.Add(
                        $"{difficultyLevel.Name}: {round.question.Left} {round.question.Operator} {round.question.Right} = {round.question.Answer} | " +
                        $"Your answer: {round.userAnswer} | {(round.correct ? "Correct" : "Incorrect")} | " +
                        $"Time: {round.elapsed.TotalSeconds:F2}s | Score: {score}");

                    EnterKeyOrCountdownThenClear(5);
                    break; 
                }

            }
            else if (string.Equals(userInput, "s", StringComparison.OrdinalIgnoreCase))
            {
                if (saveStore.Count == 0)
                {
                    Console.WriteLine("A game must be played first");
                    EnterKeyOrCountdownThenClear(2);
                    continue;
                }

                int counter = 1;
                foreach (string entry in saveStore)
                {

                    Console.WriteLine($"Game No.{counter}: \"{entry}\"");
                    counter++;
                }

                EnterKeyOrCountdownThenClear(10);
            }
            else
            {
                Console.WriteLine("Please pick a valid entry key");
            }
        }

    }

    /* Types & Config */
    struct Question
    {
        public int Left;
        public int Right;
        public char Operator;

        public int Answer => Operator switch
        {
            '+' => Left + Right,
            '-' => Left - Right,
            '*' => Left * Right,
            '/' => Left / Right,
            _ => throw new InvalidOperationException("Invalid operator")
        };
        public override string ToString() => $"{Left} {Operator} {Right} = {Answer}";
    }

    record Difficulty(string Name, int Min, int Max, int minDivisor, int maxDivisor);

    static readonly Difficulty Easy = new("Easy", 1, 50, 2, 12);
    static readonly Difficulty Hard = new("Hard", 50, 250, 3, 20);

    /* Helpers */
    static Question GenerateQuestion(char op, int min, int max, int minDivisor, int maxDivisor)
    {
        switch (op)
        {
            case '+':
            case '*':
            case '-':
                return new Question
                {
                    Left = Rng.Next(min, max + 1),
                    Right = Rng.Next(min, max + 1),
                    Operator = op
                };

            case '/':

                int divisor = Rng.Next(minDivisor, maxDivisor + 1);
                int quotient = Rng.Next(min, max + 1);

                int dividend = divisor * quotient;

                return new Question
                {
                    Left = dividend,
                    Right = divisor,
                    Operator = '/'
                };
            default:
                throw new ArgumentException("Invalid Operator Choose one of +, *, -, /");
        }
    }

    static (bool correct, TimeSpan elapsed, Question question, int updatedScore, int userAnswer) PlayRound(Difficulty difficulty, char op, int currentScore)
    {
        var question = GenerateQuestion(op, difficulty.Min, difficulty.Max, difficulty.minDivisor, difficulty.maxDivisor);
        if (difficulty == Easy)
        {
            while (op == '/' && question.Left > 144)
            {
                question = GenerateQuestion(op, difficulty.Min, difficulty.Max, difficulty.minDivisor, difficulty.maxDivisor);
            }
        }
        else
        {
            while (op == '/' && question.Left > 1000)
            {
                question = GenerateQuestion(op, difficulty.Min, difficulty.Max, difficulty.minDivisor, difficulty.maxDivisor);
            }
        }
        Console.WriteLine($"What is {question.Left} {op} {question.Right}?");

        var stopWatch = Stopwatch.StartNew();
        int userAnswer = ReadIntFromConsole();
        stopWatch.Stop();

        bool correct = userAnswer == question.Answer;
        int newScore = currentScore + (correct ? 1 : -1);

        if (correct) Console.WriteLine("That answer is corrrrrrrect!");
        else Console.WriteLine($"Sorry, the correct answer is {question.Answer}");

        return (correct, stopWatch.Elapsed, question, newScore, userAnswer);
    }

    static int ReadIntFromConsole()
    {
        while (true)
        {
            string? s = Console.ReadLine();
            if (int.TryParse(s, out var n)) return n;
            Console.WriteLine("Invalid Number. try again");
        }
    }
    static char ReadOperator()
    {
        Console.WriteLine("Pick an operator: +, -, *, /");
        while (true)
        {
            string? s = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(s))
            {
                char c = s.Trim()[0];
                if (c is '+' or '-' or '*' or '/') return c;
            }

            Console.WriteLine("Please type one of + - * / and Press Enter");
        }
    }

    static Difficulty ReadDifficulty()
    {
        Console.WriteLine("Pick a difficulty Easy(E) or Hard(H)?");

        while (true)
        {
            string? s = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(s))
            {
                char c = char.ToUpperInvariant(s.Trim()[0]);
                if (c == 'E') return Easy;
                if (c == 'H') return Hard;
            }
            Console.WriteLine("Please press E for Easy or H for Hard.");
        }
    }
    static void EnterKeyOrCountdownThenClear(int seconds)
    {

        for (int s = seconds; s > 0; s--)
        {
            Console.Write($"\rPress any key to continue. Clearing console in {s}...");
            Thread.Sleep(1000);
            if (Console.KeyAvailable)
            {
                Console.WriteLine();
                Console.Clear();
                break; 
            }
        }
        Console.Clear();
    }

}