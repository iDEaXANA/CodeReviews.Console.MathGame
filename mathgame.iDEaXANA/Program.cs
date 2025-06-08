static string GetUserInputs()
{
    Console.WriteLine("Please pick your operators by pressing the corresponding key on your keyboard");
    Console.WriteLine("+");
    Console.WriteLine("-");
    Console.WriteLine("*");
    Console.WriteLine("/");

    string operatorInput = Console.ReadLine();
    return operatorInput;
}

static (int firstNumber, int secondNumber, int dividend) GetRandomNumbers()
{
    Random rnd = new Random();
    int firstNumber = rnd.Next(1, 10);
    int secondNumber = rnd.Next(1, 10);
    int dividend = rnd.Next(1, 101);

    return (firstNumber, secondNumber, dividend);
}

static string GetDisplayResult(int firstNumber, string operatorInput, int secondNumber, int dividend, int result)
{
    if (operatorInput == "/")
    {
        return $"{dividend} {operatorInput} {secondNumber} = {result}";
    }
    return $"{firstNumber} {operatorInput} {secondNumber} = {result}";
}

static int GetUserAnswer()
{
    string? resultInput = Console.ReadLine();
    if (int.TryParse(resultInput, out int userAnswer))
    {
        return userAnswer;
    }
    Console.WriteLine("Invalid input. Please enter a valid number.");
    return GetUserAnswer();
}


List<string> saveStore = new List<string>();
Console.WriteLine("New Game");
Console.WriteLine("Saved Games");
int score = 0;
int operatorResult;

while (true)
{
    Console.WriteLine("Press the 'N' key to start a new game or the 'S' key to view saved games");
    string? userInput = Console.ReadLine();

    int result;
    if (userInput == "n" || userInput == "N")
    {
        string operatorInput = GetUserInputs();
        (int firstNumber, int secondNumber, int dividend) = GetRandomNumbers();
        if (operatorInput == "+")
        {
            // Do operation
            result = firstNumber + secondNumber;
        }
        else if (operatorInput == "-")
        {
            result = firstNumber - secondNumber;

        }
        else if (operatorInput == "*")
        {
            result = firstNumber * secondNumber;
        }
        else if (operatorInput == "/")
        {
            while (dividend % secondNumber != 0)
            {
                (firstNumber, secondNumber, dividend) = GetRandomNumbers();
            }
            result = dividend / secondNumber;
        }
        else
        {
            Console.WriteLine("Invalid Operator. Please choose from the keys displayed on the screen.");
            continue;
        }

        Console.WriteLine("Pick a difficulty Easy(E) or Hard(H)?");
        string? difficultyLevel = Console.ReadLine();
        if (difficultyLevel == "e" || difficultyLevel == "E")
        {
            var displayResult = GetDisplayResult(firstNumber, operatorInput, secondNumber, dividend, result);
            Console.WriteLine(displayResult);
            saveStore.Add(displayResult);
        }
        else if (difficultyLevel == "h" || difficultyLevel == "H")
        {

            if (operatorInput == "/")
            {
                Console.WriteLine($"What is {dividend} {operatorInput} {secondNumber}?");
                operatorResult = GetUserAnswer();

            }
            else if (operatorInput == "+" || operatorInput == "-" || operatorInput == "*")
            {
                Console.WriteLine($"What is {firstNumber} {operatorInput} {secondNumber}?");
                operatorResult = GetUserAnswer();
            }
            else
            {
                Console.WriteLine("Invalid Operator. Please choose from the keys displayed on the screen.");
                continue;
            }

            if (operatorResult == result)
            {
                score++;
                Console.WriteLine("That is the correct answer!");
            }
            else
            {
                score = 0;
                Console.WriteLine("That is the wrong answer! Your score streak has been reset");
            }
            var displayResult = GetDisplayResult(firstNumber, operatorInput, secondNumber, dividend, result);
            Console.WriteLine(displayResult);
            Console.WriteLine($" Score: {score}");
            saveStore.Add(displayResult);
        }
        else
        {
            Console.WriteLine("Invalid difficulty level. Please choose 'E' for Easy or 'H' for Hard.");
            continue;
        }

    }
    else if (userInput == "s" || userInput == "S")
    {
        if (saveStore.Count == 0)
        {
            Console.WriteLine("A game must be played first");
            continue;
        }
        int counter = 1;
        foreach (string entry in saveStore)
        {

            Console.WriteLine($"Game No.{counter}: \"{entry}\"");
            counter++;
        }
    }
    else
    {
        Console.WriteLine("Please pick a valid entry key");
    }
}

/*
 WORK LEFT:

 *Add a timer:
 *When game starts
 *Ends when value matches correctly.
 *COMMIT
 
 *AI Challenge
 *COMMIT
*/

