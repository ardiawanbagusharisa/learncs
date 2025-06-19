string[][] userEnteredValues =
[
    ["1", "2", "3"],
    ["1", "two", "3"],
    ["0", "1", "2"]
];

try
{
    //Workflow1(userEnteredValues);
    Console.WriteLine("'Workflow1' completed successfully.");
}
catch (DivideByZeroException e)
{
    Console.WriteLine("An error occurred during 'Workflow1'.");
    Console.WriteLine(e.Message);
}

static void Workflow1(string[][] userEnteredValues)
{
    foreach (string[] userEntries in userEnteredValues)
    {
        try
        {
            Process1(userEntries);
            Console.WriteLine("'Process1' completed successfully.");
            Console.WriteLine();
        }
        catch (FormatException e)
        {
            Console.WriteLine("'Process1' encountered an issue, process aborted.");
            Console.WriteLine(e.Message);
            Console.WriteLine();
        }
    }
}

static void Process1(String[] userEntries)
{
    int valueEntered;

    foreach (string userValue in userEntries)
    {
        bool integerFormat = int.TryParse(userValue, out valueEntered);
        Console.WriteLine("Uservalue: " + userValue);

        if (integerFormat == true)
        {
            if (valueEntered != 0)
            {
                checked
                {
                    int calculatedValue = 4 / valueEntered;
                }
            }
            else
            {
                throw new DivideByZeroException("Invalid data. User input values must be non-zero values.");
            }
        }
        else
        {
            throw new FormatException("Invalid data. User input values must be valid integers.");
        }
    }
}



// checked
// {
//     try
//     {
//         int num1 = int.MaxValue;
//         int num2 = int.MaxValue;
//         int result = num1 + num2;
//         Console.WriteLine("Result: " + result);
//     }
//     catch (OverflowException ex)
//     {
//         Console.WriteLine("Error: The number is too large to be represented as an integer." + ex.Message);
//     }
// }
// try
// {
//     string str = null;
//     int length = str.Length;
//     Console.WriteLine("String Length: " + length);
// }
// catch (NullReferenceException ex)
// {
//     Console.WriteLine("Error: The reference is null." + ex.Message);
// }
// try
// {
//     int[] numbers = new int[5];
//     numbers[5] = 10;
//     Console.WriteLine("Number at index 5: " + numbers[5]);
// }
// catch (IndexOutOfRangeException ex)
// {
//     Console.WriteLine("Error: Index out of range." + ex.Message);
// }

// try
// {
//     int num3 = 10;
//     int num4 = 0;
//     int result2 = num3 / num4;
//     Console.WriteLine("Result: " + result2);
// }
// catch (DivideByZeroException ex)
// {
//     Console.WriteLine("Error: Cannot divide by zero." + ex.Message);
// }

// Console.WriteLine("Exiting program.");

// string[] input = ["three", "99999999999", "0", "2"];

// foreach (string s in input)
// {
//     int num = 0;
//     try
//     {
//         num = int.Parse(s);
//     }
//     catch (FormatException)
//     {
//         Console.WriteLine("Invalid. Enter a valid number");
//     }
//     catch (OverflowException)
//     {
//         Console.WriteLine("Number is too large or too small");
//     }
//     catch (Exception e)
//     {
//         Console.WriteLine(e.Message);
//     }
// }


// try
// {
//     Proces1();
// }
// catch
// {
//     Console.WriteLine("An exception has been caught");
// }

// Console.WriteLine("Exit program");
// Console.ReadLine();

// void Proces1()
// {
//     try
//     {
//         WriteMessage();
//     }
//     catch (DivideByZeroException e)
//     {
//         Console.WriteLine($"Exception caught in Process1: {e.Message}");
//     }

// }

// void WriteMessage()
// {
//     double num1 = 3000.0;
//     double num2 = 0.0;
//     int n1 = 3000;
//     int n2 = 0;
//     byte smallNum;

//     try
//     {
//         Console.WriteLine(num1 / num2);
//         Console.WriteLine(n1 / n2);
//     }
//     catch (DivideByZeroException e)
//     {
//         Console.WriteLine($"Exception caught in WriteMessage: {e.Message}");
//     }
//     checked
//     {
//         try
//         {
//             smallNum = (byte)n1;
//         }
//         catch (OverflowException e)
//         {
//             Console.WriteLine($"Exception caught in WriteMessage: {e.Message}");
//         }
//     }
// }
// ---

// string? readResult;
// int startIndex = 0;
// bool goodEntry = false;

// int[] numbers = { 1, 2, 3, 4, 5 };

// // Display the array to the console.
// Console.Clear();
// Console.Write("\n\rThe 'numbers' array contains: { ");
// foreach (int number in numbers)
// {
//     Console.Write($"{number} ");
// }

// // To calculate a sum of array elements, 
// //  prompt the user for the starting element number.
// Console.WriteLine($"}}\n\r\n\rTo sum values 'n' through 5, enter a value for 'n':");
// while (goodEntry == false)
// {
//     readResult = Console.ReadLine();
//     goodEntry = int.TryParse(readResult, out startIndex);

//     if (startIndex > 5)
//     {
//         goodEntry = false;
//         Console.WriteLine("\n\rEnter an integer value between 1 and 5");
//     }
// }

// // Display the sum and then pause.
// Console.WriteLine($"\n\rThe sum of numbers {startIndex} through {numbers.Length} is: {SumValues(numbers, startIndex-1)}");

// Console.WriteLine("press Enter to exit");
// readResult = Console.ReadLine();

// // This method returns the sum of elements n through 5
// static int SumValues(int[] numbers, int n)
// {
//     int sum = 0;
//     for (int i = n; i < numbers.Length; i++)
//     {
//         sum += numbers[i];
//     }
//     return sum;
// }

// ---

// using System.Diagnostics;

// int productCount = 2000;
// string[,] products = new string[productCount, 2];

// LoadProduct(products, productCount);

// for (int i = 0; i < productCount; i++)
// {
//     string result;
//     result = Process1(products, i);
//     if (result != "obsolete")
//     {
//         result = Process2(products, i);
//     }
// }
// bool pauseCode = true;
// while (pauseCode) ;

// // ---
// void LoadProduct(string[,] products, int productCount)
// {
//     Random random = new Random();

//     for (int i = 0; i < productCount; i++)
//     {
//         int num1 = random.Next(1, 10000) + 10000;
//         int num2 = random.Next(1, 101);
//         string prodID = num1.ToString();

//         if (num2 < 91) products[i, 1] = "Existing";
//         else if (num2 == 91)
//         {
//             products[i, 1] = "new";
//             prodID = prodID + "-n";
//         }
//         else
//         {
//             products[i, 1] = "obsolete";
//             prodID = prodID + "-0";
//         }
//         products[i, 0] = prodID;
//     }
// }

// string Process1(string[,] products, int item)
// {
//     Console.WriteLine($"Process1 message - working on {products[item, 1]} product");
//     return products[item, 1];
// }

// string Process2(string[,] products, int item)
// {
//     Console.WriteLine($"Process2 message - working on product ID: {products[item, 0]}");
//     if (products[item, 1] == "new") Process3(products, item);
//     return "continue";
// }

// void Process3(string[,] products, int item)
// {
//     Console.WriteLine($"Process3 message - processing product information for 'new' product");
// }