// CSE 210 - Journal Program
//
// This program is a digital journal that allows the user to write,
// display, save, and load journal entries.
//
// The program uses several classes to organize its responsibilities:
// - Program: Controls the menu and user interaction.

// - Entry: Represents an individual journal entry, including its date,
//   prompt, and response.

// - Journal: Stores and manages the journal entries, including displaying,
//   saving, and loading them.

// - PromptGenerator: Stores the writing prompts and randomly selects one
//   for the user.
//
// The program also uses files so journal entries can be saved and loaded
// between different times the program is run.

//
// Creativity / Exceeding Requirements:
// I added a mood field to each journal entry so the user can record
// additional information beyond the required date, prompt, and response.
// The mood is also saved to the file and loaded with the journal entry.

using System;
using System.Collections.Generic;
using System.IO;


// PROGRAM 
class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();

        bool running = true;

        while (running)
        {
            DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    string prompt = promptGenerator.GetRandomPrompt();

                    Console.WriteLine($"Prompt: {prompt}");
                    Console.Write("> ");
                    string response = Console.ReadLine();

                    Console.Write("How are you feeling today? ");
                    string mood = Console.ReadLine();

                    Entry entry = new Entry(prompt, response, mood);
                    journal.AddEntry(entry);

                    Console.WriteLine("Entry saved!");
                    break;

                case "2":
                    journal.Display();
                    break;

                case "3":
                    Console.Write("Enter filename to save: ");
                    string saveFile = Console.ReadLine();
                    journal.SaveToFile(saveFile);
                    break;

                case "4":
                    Console.Write("Enter filename to load: ");
                    string loadFile = Console.ReadLine();
                    journal.LoadFromFile(loadFile);
                    break;

                case "5":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid option. Please choose 1-5.");
                    break;
            }
        }
    }

    static void DisplayMenu()
    {
        Console.WriteLine("\n===== Journal Menu =====");
        Console.WriteLine("1. Write a new entry");
        Console.WriteLine("2. Display the journal");
        Console.WriteLine("3. Save journal to file");
        Console.WriteLine("4. Load journal from file");
        Console.WriteLine("5. Quit");
        Console.Write("What would you like to do? ");
    }
}

// ENTRY
public class Entry
{
    private string _date;
    private string _prompt;
    private string _response;
    private string _mood;

    // New Entry
    public Entry(string prompt, string response, string mood)
    {
        _date = DateTime.Now.ToShortDateString();
        _prompt = prompt;
        _response = response;
        _mood = mood;
    }

    // File Entry
    public Entry(string date, string prompt, string response, string mood)
    {
        _date = date;
        _prompt = prompt;
        _response = response;
        _mood = mood;
    }

    public void Display()
    {
        Console.WriteLine($"Date: {_date}");
        Console.WriteLine($"Prompt: {_prompt}");
        Console.WriteLine($"Response: {_response}");
        Console.WriteLine($"Mood: {_mood}");
    }

    // Converts an Entry into one line for the file
    public string ToFileLine()
    {
        return $"{_date}|{_prompt}|{_response}|{_mood}";
    }

    // Converts one line from the file back into an Entry
    public static Entry FromFileLine(string line)
    {
        string[] parts = line.Split('|');

        return new Entry(parts[0], parts[1], parts[2], parts[3]);
    }
}

// JOUNRAL 
public class Journal
{
    private List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void Display()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries yet.");
            return;
        }

        foreach (Entry entry in _entries)
        {
            entry.Display();
            Console.WriteLine(new string('-', 50));
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Entry entry in _entries)
            {
                writer.WriteLine(entry.ToFileLine());
            }
        }

        Console.WriteLine($"Journal saved to '{filename}'.");
    }

    public void LoadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine($"File '{filename}' not found.");
            return;
        }

        _entries.Clear();

        string[] lines = File.ReadAllLines(filename);

        foreach (string line in lines)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                Entry entry = Entry.FromFileLine(line);
                _entries.Add(entry);
            }
        }

        Console.WriteLine($"Journal loaded from '{filename}'.");
    }
}


//PROMPTGENERATOR
public class PromptGenerator
{
    private List<string> _prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "What was the strongest emotion I felt today?",
        "What is something I learned today?",
        "What am I grateful for today?"
    };

    private Random _random = new Random();

    public string GetRandomPrompt()
    {
        int index = _random.Next(_prompts.Count);
        return _prompts[index];
    }
}