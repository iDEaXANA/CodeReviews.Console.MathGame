// 1. Display + Conditionals DONE
// 3. Check for non int calculations. DONE
// 4. Display List appropriately DONE
// 5. Need to retrigger user input after:
//      a. each result 
//      b. when dividing results in non integer value.

List<string> saveStore = new List<string>();
    Console.WriteLine("New Game");
    Console.WriteLine("Saved Games"); 

    while (true)
    {
    Console.WriteLine("Press the 'N' key to start a new game or the 'S' key to view saved games");

    string? userInput = Console.ReadLine();

    

    static (int firstNumber, int secondNumber) GetUserInputs()
    {
        Console.WriteLine("Pick your first number. It can be any number, provided both numbers result in an integer.");
        string? firstNumberInput = Console.ReadLine();
        if (firstNumberInput == null) throw new InvalidOperationException("stop trolling");
        int firstNumber = int.Parse(firstNumberInput);
        Console.WriteLine("Don't worry... I'll let you know if the end result is an integer!");
        Console.WriteLine("Now pick your second number. If dividing, this number must be between 0 and 100!");
        string? secondNumberInput = Console.ReadLine();
        if (secondNumberInput == null) throw new InvalidOperationException("Really... Again?");
        int secondNumber = int.Parse(secondNumberInput);
        Console.WriteLine("Now, please pick your operators by pressing the corresponding key on your keyboard");
        Console.WriteLine("+");
        Console.WriteLine("-");
        Console.WriteLine("*");
        Console.WriteLine("/");

        return (firstNumber, secondNumber);
    }

    if (userInput == "n" || userInput == "N")
    {
        (int firstNumber, int secondNumber) = GetUserInputs();
        var operatorInput = Console.ReadLine();

        if (operatorInput == "+")
        {
            // Do operation
            var result = firstNumber + secondNumber;
            // print operation
            var displayResult = $"{firstNumber} {operatorInput} {secondNumber} = {result}";
            Console.WriteLine(displayResult);
            // save operation to a new list
            saveStore.Add(displayResult);
        }
        else if (operatorInput == "-")
        {
            var result = firstNumber - secondNumber;
            var displayResult = $"{firstNumber} {operatorInput} {secondNumber} = {result}";

            Console.WriteLine(displayResult);
            saveStore.Add(displayResult);
        }
        else if (operatorInput == "*")
        {
            var result = firstNumber * secondNumber;
            var displayResult = $"{firstNumber} {operatorInput} {secondNumber} = {result}";

            Console.WriteLine(displayResult);
            saveStore.Add(displayResult);
        }
        else if (operatorInput == "/")
        {
            if (secondNumber <= 0 || secondNumber >= 101)
            {
                Console.WriteLine("The Dividend must be between 0 and 100, including 100");
                GetUserInputs();
            }
            var result = firstNumber / secondNumber;
            if (result % 1 != 0)
            {
                Console.WriteLine("The result is not an integer. Restarting game...");
                GetUserInputs();
            }

            var displayResult = $"{firstNumber} {operatorInput} {secondNumber} = {result}";

            Console.WriteLine(displayResult);
            saveStore.Add(displayResult);
        }
    }
    else if (userInput == "s" || userInput == "S")
    {
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

/* Random game To Do
 * Replace user input values with random values
 * For added difficulty, add a digit
 * Pick two random numbers
 * To follow division integer rule, ??
 * Save results in a new list
 * Display results based on gametype

 *
 */ 


/*CHALLENGES
 *
 * Random questions game
 * Timer 
 * Azure AI input to ask questions
 * Get a code review.
 * 
 * 
 */