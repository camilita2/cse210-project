
using System;
using System.Collections.Generic;
using System.IO;

// Creativity feature:
// The program has a simple leveling system. Every 500 points,
// the player reaches a new level and earns a new title.

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Eternal Quest!");

        GoalManager manager = new GoalManager();
        manager.Start();
    }
}

// This is the base class. All goals share these attributes.
public abstract class Goal
{
    private string _name;
    private string _description;
    private int _points;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    // These methods let child classes use the shared information.
    public string GetName()
    {
        return _name;
    }

    public int GetPoints()
    {
        return _points;
    }

    public string GetDescription()
    {
        return _description;
    }

    // Child classes must provide their own versions of these methods.
    public abstract int RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetStringRepresentation();

    public virtual string GetDetailsString()
    {
        string status = "[ ]";

        if (IsComplete())
        {
            status = "[X]";
        }

        return status + " " + _name + " (" + _description + ")";
    }
}

// A simple goal can only be completed once.
public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points,
        bool isComplete = false)
        : base(name, description, points)
    {
        _isComplete = isComplete;
    }

    public override int RecordEvent()
    {
        if (_isComplete)
        {
            return 0;
        }

        _isComplete = true;
        return GetPoints();
    }

    public override bool IsComplete()
    {
        return _isComplete;
    }

    public override string GetStringRepresentation()
    {
        return "SimpleGoal|" + GetName() + "|" +
            GetDescription() + "|" + GetPoints() + "|" + _isComplete;
    }
}

// An eternal goal can be recorded over and over.
public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    public override int RecordEvent()
    {
        return GetPoints();
    }

    public override bool IsComplete()
    {
        return false;
    }

    public override string GetStringRepresentation()
    {
        return "EternalGoal|" + GetName() + "|" +
            GetDescription() + "|" + GetPoints();
    }
}

// A checklist goal gives points each time and a bonus at the end.
public class ChecklistGoal : Goal
{
    private int _target;
    private int _bonus;
    private int _amountCompleted;

    public ChecklistGoal(string name, string description, int points,
        int target, int bonus, int amountCompleted = 0)
        : base(name, description, points)
    {
        _target = target;
        _bonus = bonus;
        _amountCompleted = amountCompleted;
    }

    public override int RecordEvent()
    {
        if (IsComplete())
        {
            return 0;
        }

        _amountCompleted++;

        if (_amountCompleted == _target)
        {
            Console.WriteLine("You earned a bonus of " + _bonus + " points!");
            return GetPoints() + _bonus;
        }

        return GetPoints();
    }

    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }

    public override string GetDetailsString()
    {
        string details = base.GetDetailsString();

        details += " -- Completed " + _amountCompleted +
            "/" + _target + " times";

        return details;
    }

    public override string GetStringRepresentation()
    {
        return "ChecklistGoal|" + GetName() + "|" +
            GetDescription() + "|" + GetPoints() + "|" +
            _target + "|" + _bonus + "|" + _amountCompleted;
    }
}

// This class manages the goals, score, and menu.
public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public void Start()
    {
        int choice = 0;

        while (choice != 6)
        {
            DisplayPlayerInfo();

            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");
            Console.Write("Choose an option: ");

            choice = ReadInt();

            if (choice == 1)
            {
                CreateGoal();
            }
            else if (choice == 2)
            {
                ListGoals();
            }
            else if (choice == 3)
            {
                SaveGoals();
            }
            else if (choice == 4)
            {
                LoadGoals();
            }
            else if (choice == 5)
            {
                RecordEvent();
            }
            else if (choice == 6)
            {
                Console.WriteLine("Goodbye!");
            }
            else
            {
                Console.WriteLine("Please choose a number from 1 to 6.");
            }
        }
    }

    // Creativity feature: every 500 points earns a new level.
    private void DisplayPlayerInfo()
    {
        int level = 1 + _score / 500;

        string title = "Beginner";

        if (level >= 2)
        {
            title = "Quest Explorer";
        }

        if (level >= 3)
        {
            title = "Quest Champion";
        }

        if (level >= 4)
        {
            title = "Eternal Master";
        }

        Console.WriteLine();
        Console.WriteLine("Your score: " + _score);
        Console.WriteLine("Level " + level + ": " + title);
    }

    private void CreateGoal()
    {
        Console.WriteLine();
        Console.WriteLine("Choose a goal type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Your choice: ");

        int type = ReadInt();

        if (type < 1 || type > 3)
        {
            Console.WriteLine("Invalid goal type.");
            return;
        }

        Console.Write("Enter the goal name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Enter the goal description: ");
        string description = Console.ReadLine() ?? "";

        Console.Write("How many points is it worth? ");
        int points = ReadInt();

        if (type == 1)
        {
            Goal goal = new SimpleGoal(name, description, points);
            _goals.Add(goal);
        }
        else if (type == 2)
        {
            Goal goal = new EternalGoal(name, description, points);
            _goals.Add(goal);
        }
        else
        {
            Console.Write("How many times must you complete it? ");
            int target = ReadInt();

            while (target < 1)
            {
                Console.Write("Enter a number greater than zero: ");
                target = ReadInt();
            }

            Console.Write("How many bonus points do you earn at the end? ");
            int bonus = ReadInt();

            Goal goal = new ChecklistGoal(
                name, description, points, target, bonus);

            _goals.Add(goal);
        }

        Console.WriteLine("Goal created!");
    }

    private void ListGoals()
    {
        Console.WriteLine();
        Console.WriteLine("Your Goals:");

        if (_goals.Count == 0)
        {
            Console.WriteLine("You have not created any goals yet.");
            return;
        }

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " +
                _goals[i].GetDetailsString());
        }
    }

    private void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("Create a goal first.");
            return;
        }

        ListGoals();

        Console.Write("Which goal did you complete? ");
        int number = ReadInt();

        if (number < 1 || number > _goals.Count)
        {
            Console.WriteLine("Invalid goal number.");
            return;
        }

        int pointsEarned = _goals[number - 1].RecordEvent();

        _score += pointsEarned;

        if (pointsEarned > 0)
        {
            Console.WriteLine("Congratulations! You earned " +
                pointsEarned + " points!");
        }
        else
        {
            Console.WriteLine("This goal is already complete.");
        }

        DisplayPlayerInfo();
    }

    private void SaveGoals()
    {
        Console.Write("Enter a filename, such as goals.txt: ");
        string filename = Console.ReadLine() ?? "goals.txt";

        using (StreamWriter writer = new StreamWriter(filename))
        {
            // Save the score first.
            writer.WriteLine(_score);

            // Save each goal on its own line.
            foreach (Goal goal in _goals)
            {
                writer.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine("Goals saved!");
    }

    private void LoadGoals()
    {
        Console.Write("Enter the filename to load: ");
        string filename = Console.ReadLine() ?? "goals.txt";

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);

        if (lines.Length == 0)
        {
            Console.WriteLine("The file is empty.");
            return;
        }

        _goals.Clear();
        _score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');

            if (parts[0] == "SimpleGoal")
            {
                Goal goal = new SimpleGoal(
                    parts[1],
                    parts[2],
                    int.Parse(parts[3]),
                    bool.Parse(parts[4]));

                _goals.Add(goal);
            }
            else if (parts[0] == "EternalGoal")
            {
                Goal goal = new EternalGoal(
                    parts[1],
                    parts[2],
                    int.Parse(parts[3]));

                _goals.Add(goal);
            }
            else if (parts[0] == "ChecklistGoal")
            {
                Goal goal = new ChecklistGoal(
                    parts[1],
                    parts[2],
                    int.Parse(parts[3]),
                    int.Parse(parts[4]),
                    int.Parse(parts[5]),
                    int.Parse(parts[6]));

                _goals.Add(goal);
            }
        }

        Console.WriteLine("Goals loaded!");
    }

    // This method keeps asking until the user enters a whole number.
    private int ReadInt()
    {
        int number;

        while (!int.TryParse(Console.ReadLine(), out number))
        {
            Console.Write("Please enter a whole number: ");
        }

        return number;
    }
}
