// 1. Display + Conditionals DONE
// 3. Check for non int calculations. DONE
// 4. Display List appropriately DONE
// 5. Need to retrigger user input after:
//      a. each result 
//      b. when dividing results in non integer value.

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


List<string> saveStore = new List<string>();
Console.WriteLine("New Game");
Console.WriteLine("Saved Games");

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
        var displayResult = GetDisplayResult(firstNumber, operatorInput, secondNumber, dividend, result);
        Console.WriteLine(displayResult);
        saveStore.Add(displayResult);
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
// Current problems



/* Random game To Do
 * Replace user input values with random values
 * COMMIT
 * For added difficulty, add a digit to dividend only.
 * Add a pre check for calculation
 * Allow user input and make sure the values match.
 * Add Easy mode and Hard mode from menu for different pathways.
 * COMMIT
 * 

 *Add a timer:
 *When game starts
 *Ends when value matches correctly.
 *COMMIT
 *
 *Add Random Operations
 *COMMIT
 
 *AI Challenge
 *COMMIT
 */

