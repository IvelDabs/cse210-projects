using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your grade percentage? ");
        string answer = Console.ReadLine();
        int percent = int.Parse(answer);

        string gradeLetter = "";

        if (percent >= 90)
        {
            gradeletter = "A";
        }
        else if (percent >= 80)
        {
            gradeletter = "B";
        }
        else if (percent >= 70)
        {
            gradeLetter = "C";
        }
        else if (percent >= 60)
        {
            gradeLetter = "D";
        }
        else
        {
            gradeLetter = "F";
        }

        Console.WriteLine($"Your grade is: {gradeLetter}");
        
        if (percent >= 70)
        {
            Console.WriteLine("You passed!");
        }
        else
        {
            Console.WriteLine("Better luck next time!");
        }
    }
}
