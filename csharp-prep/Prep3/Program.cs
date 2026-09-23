using System;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();
        int magicNumber = random.Next(1, 101);

        int magicGuess = -1;

        while (magicGuess != magicNumber)
        {
        Console.Write("What is your guess? ");
        string guess = Console.ReadLine();
        magicGuess = int.Parse(guess);

        if (magicGuess > magicNumber)
        {
            Console.WriteLine("Lower");
        }
        else if (magicGuess < magicNumber)
        {
            Console.WriteLine("Higher");
        }
        else
        {
            Console.WriteLine("You guessed it!");
        }
        }
        
    }
}