using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

public class Activity
{
    private string _name;
    private string _description;
    protected int _duration; // in seconds

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public void SetDuration(int duration)
    {
        _duration = duration;
    }

    public void Start()
    {
        Console.WriteLine($"Starting {_name}: {_description}");
        Pause(3);
    }

    public void End()
    {
        Console.WriteLine("Good job! You've completed the activity.");
        Console.WriteLine($"Duration: {_duration} seconds");
        LogActivity(_name);
        Pause(3);
    }

    protected void Pause(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }

    // Feature: Logging Activity
    protected void LogActivity(string activityName)
    {
        using (StreamWriter sw = new StreamWriter("activity_log.txt", true))
        {
            sw.WriteLine($"{DateTime.Now}: Completed {activityName}");
        }
    }
}

public class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing Activity", 
        "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public void StartBreathingSession()
    {
        Start();
        for (int i = 0; i < _duration; i += 4)
        {
            BreathingAnimation();
        }
        End();
    }

    // Feature: Breathing Animation
    private void BreathingAnimation()
    {
        Console.Write("Inhale ");
        for (int i = 0; i < 10; i++)
        {
            Console.Write("▲");
            Thread.Sleep(100);
        }
        Console.WriteLine();
        Console.Write("Exhale ");
        for (int i = 0; i < 10; i++)
        {
            Console.Write("▼");
            Thread.Sleep(100);
        }
        Console.WriteLine();
    }
}

public class ReflectionActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need."
    };

    public ReflectionActivity() : base("Reflection Activity", 
        "This activity will help you reflect on times in your life when you have shown strength and resilience.")
    {
    }

    public void StartReflectionSession()
    {
        Start();
        Random rand = new Random();
        string prompt = _prompts[rand.Next(_prompts.Count)];
        Console.WriteLine(prompt);
        Pause(5);

        List<string> questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "How did you feel when it was complete?",
            "What is your favorite thing about this experience?"
        };

        foreach (var question in questions)
        {
            Console.WriteLine(question);
            Pause(5);
        }
        End();
    }
}

public class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() : base("Listing Activity", 
        "This activity will help you reflect on the good things in your life by having you list as many things as you can.")
    {
    }

    public void StartListingSession()
    {
        Start();
        Random rand = new Random();
        string prompt = _prompts[rand.Next(_prompts.Count)];
        Console.WriteLine(prompt);
        Pause(5);

        Console.WriteLine("Start listing! Type 'done' when finished:");
        List<string> items = new List<string>();
        string input;

        while ((input = Console.ReadLine()) != "done")
        {
            items.Add(input);
            Console.WriteLine("Great! Keep going...");
        }

        Console.WriteLine($"You listed {items.Count} items.");
        End();
    }
}

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Select an activity:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("0. Exit");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var breathing = new BreathingActivity();
                    breathing.SetDuration(30); // Set desired duration
                    breathing.StartBreathingSession();
                    break;
                case "2":
                    var reflection = new ReflectionActivity();
                    reflection.SetDuration(30);
                    reflection.StartReflectionSession();
                    break;
                case "3":
                    var listing = new ListingActivity();
                    listing.SetDuration(30);
                    listing.StartListingSession();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
