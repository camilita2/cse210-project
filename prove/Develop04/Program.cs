
using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        string choice = "";

        while (choice != "4")
        {
            Console.Clear();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflecting activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. Quit");
            Console.Write("Select a choice from the menu: ");
            choice = Console.ReadLine();

            if (choice == "1")
            {
                BreathingActivity activity = new BreathingActivity();
                activity.Start();
            }
            else if (choice == "2")
            {
                ReflectionActivity activity = new ReflectionActivity();
                activity.Start();
            }
            else if (choice == "3")
            {
                ListingActivity activity = new ListingActivity();
                activity.Start();
            }
        }
    }
}

// Base class for all activities
public abstract class Activity
{
    private string _name;
    private string _description;
    private int _duration;

    protected Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    protected int GetDuration()
    {
        return _duration;
    }

    public void Start()
    {
        Console.Clear();
        Console.WriteLine(_name);
        Console.WriteLine();
        Console.WriteLine(_description);
        Console.WriteLine();

        Console.Write("How long, in seconds, would you like for your session? ");
        _duration = int.Parse(Console.ReadLine());

        Console.Clear();
        Console.WriteLine("Get ready...");
        Pause(3);

        RunActivity();

        Console.WriteLine();
        Console.WriteLine("Well done!!");
        Pause(2);

        Console.WriteLine("You have completed another "
            + _duration + " seconds of the " + _name + ".");
        Pause(3);
    }

    protected abstract void RunActivity();

    protected void Pause(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i + " ");
            Thread.Sleep(1000);
        }

        Console.WriteLine();
    }

    protected void ShowSpinner(int seconds)
    {
        string[] symbols = { "|", "/", "-", "\\" };

        for (int i = 0; i < seconds * 4; i++)
        {
            Console.Write(symbols[i % 4]);
            Thread.Sleep(250);
            Console.Write("\b \b");
        }
    }
}

// Breathing activity
public class BreathingActivity : Activity
{
    public BreathingActivity()
        : base("Breathing Activity",
            "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    protected override void RunActivity()
    {
        DateTime start = DateTime.Now;

        while ((DateTime.Now - start).TotalSeconds < GetDuration())
        {
            Console.WriteLine("Breathe in...");
            ShowSpinner(3);

            if ((DateTime.Now - start).TotalSeconds < GetDuration())
            {
                Console.WriteLine();
                Console.WriteLine("Breathe out...");
                ShowSpinner(3);
            }

            Console.WriteLine();
        }
    }
}

// Reflection activity
public class ReflectionActivity : Activity
{
    private string[] _prompts =
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] _questions =
    {
        "Why was this experience meaningful to you?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What did you learn about yourself?",
        "What could you learn from this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity()
        : base("Reflection Activity",
            "This activity will help you reflect on times when you have shown strength and resilience.")
    {
    }

    protected override void RunActivity()
    {
        Random random = new Random();

        Console.WriteLine("Consider the following prompt:");
        Console.WriteLine(_prompts[random.Next(_prompts.Length)]);
        Console.WriteLine();

        Console.WriteLine("Think about this experience.");
        ShowSpinner(3);

        DateTime start = DateTime.Now;

        while ((DateTime.Now - start).TotalSeconds < GetDuration())
        {
            Console.WriteLine();
            Console.WriteLine(_questions[random.Next(_questions.Length)]);
            ShowSpinner(5);
        }
    }
}

// Listing activity
public class ListingActivity : Activity
{
    private string[] _prompts =
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity()
        : base("Listing Activity",
            "This activity will help you reflect on the good things in your life by listing as many things as you can.")
    {
    }

    protected override void RunActivity()
    {
        Random random = new Random();

        Console.WriteLine("List as many responses as you can to this prompt:");
        Console.WriteLine(_prompts[random.Next(_prompts.Length)]);
        Console.WriteLine();

        Console.WriteLine("You may begin in:");
        Pause(5);

        List<string> answers = new List<string>();
        DateTime start = DateTime.Now;

        Console.WriteLine("Enter each answer and press Enter.");

        while ((DateTime.Now - start).TotalSeconds < GetDuration())
        {
            Console.Write("> ");
            string answer = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(answer))
            {
                answers.Add(answer);
            }
        }

        Console.WriteLine();
        Console.WriteLine("You listed " + answers.Count + " items.");
    }
}
