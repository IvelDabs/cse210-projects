using System;
using System.Collections.Generic;
using System.IO;

public abstract class Goal
{
    private string _name;
    protected string _description;
    protected int _points;

    public string Name
    {
        get { return _name; }
    }

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public abstract void RecordEvent();
    public abstract void DisplayGoal();
    public abstract string GetSaveData(); // Added for saving goals
}

public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points)
        : base(name, description, points)
    {
        _isComplete = false;
    }

    public override void RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            Console.WriteLine($"Goal '{Name}' completed! You earned {_points} points.");
        }
        else
        {
            Console.WriteLine($"Goal '{Name}' is already completed.");
        }
    }

    public override void DisplayGoal()
    {
        Console.WriteLine($"[ {(_isComplete ? "X" : " ")} ] {Name}: {_description} ({_points} points)");
    }

    public override string GetSaveData()
    {
        return $"SimpleGoal|{Name}|{_description}|{_points}|{_isComplete}";
    }
}

public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override void RecordEvent()
    {
        Console.WriteLine($"Recorded an event for '{Name}'. You earned {_points} points.");
    }

    public override void DisplayGoal()
    {
        Console.WriteLine($"[ ] {Name}: {_description} (Earn points each time)");
    }

    public override string GetSaveData()
    {
        return $"EternalGoal|{Name}|{_description}|{_points}";
    }
}

public class ChecklistGoal : Goal
{
    private int _target;
    private int _completed;

    public ChecklistGoal(string name, string description, int points, int target)
        : base(name, description, points)
    {
        _target = target;
        _completed = 0;
    }

    public override void RecordEvent()
    {
        if (_completed < _target)
        {
            _completed++;
            Console.WriteLine($"Recorded an event for '{Name}'. You earned {_points} points.");
            if (_completed == _target)
            {
                Console.WriteLine($"Goal '{Name}' completed! You earned a bonus of 500 points.");
            }
        }
        else
        {
            Console.WriteLine($"Goal '{Name}' is already completed.");
        }
    }

    public override void DisplayGoal()
    {
        Console.WriteLine($"[ {(_completed >= _target ? "X" : " ")} ] {Name}: {_description} (Completed {_completed}/{_target})");
    }

    public override string GetSaveData()
    {
        return $"ChecklistGoal|{Name}|{_description}|{_points}|{_target}|{_completed}";
    }
}

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();

    public void CreateGoal()
    {
        Console.WriteLine("Enter goal type (Simple, Eternal, Checklist): ");
        string type = Console.ReadLine();

        Console.WriteLine("Enter goal name: ");
        string name = Console.ReadLine();

        Console.WriteLine("Enter goal description: ");
        string description = Console.ReadLine();

        Console.WriteLine("Enter points for the goal: ");
        int points = int.Parse(Console.ReadLine());

        if (type.ToLower() == "simple")
        {
            _goals.Add(new SimpleGoal(name, description, points));
        }
        else if (type.ToLower() == "eternal")
        {
            _goals.Add(new EternalGoal(name, description, points));
        }
        else if (type.ToLower() == "checklist")
        {
            Console.WriteLine("Enter target number of completions: ");
            int target = int.Parse(Console.ReadLine());
            _goals.Add(new ChecklistGoal(name, description, points, target));
        }
    }

    public void DisplayGoals()
    {
        foreach (var goal in _goals)
        {
            goal.DisplayGoal();
        }
    }

    public void RecordGoalEvent()
    {
        Console.WriteLine("Enter the name of the goal to record: ");
        string name = Console.ReadLine();

        foreach (var goal in _goals)
        {
            if (goal.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                goal.RecordEvent();
                return;
            }
        }

        Console.WriteLine("Goal not found.");
    }

    public void SaveGoals(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var goal in _goals)
            {
                writer.WriteLine(goal.GetSaveData());
            }
        }
        Console.WriteLine("Goals saved successfully.");
    }

    public void LoadGoals(string filename)
    {
        if (File.Exists(filename))
        {
            _goals.Clear();
            string[] lines = File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                string[] parts = line.Split('|');
                if (parts[0] == "SimpleGoal")
                {
                    var goal = new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]));
                    // Set completion state
                    goal.RecordEvent(); // Adjust if needed for completion state
                    _goals.Add(goal);
                }
                else if (parts[0] == "EternalGoal")
                {
                    _goals.Add(new EternalGoal(parts[1], parts[2], int.Parse(parts[3])));
                }
                else if (parts[0] == "ChecklistGoal")
                {
                    var checklistGoal = new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]));
                    // Set completed count
                    for (int i = 0; i < int.Parse(parts[5]); i++)
                    {
                        checklistGoal.RecordEvent(); // Increment completion
                    }
                    _goals.Add(checklistGoal);
                }
            }
            Console.WriteLine("Goals loaded successfully.");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        GoalManager goalManager = new GoalManager();

        while (true)
        {
            Console.WriteLine("1. Create new Goals");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");
            Console.Write("Select an option: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    goalManager.CreateGoal();
                    break;
                case 2:
                    goalManager.DisplayGoals();
                    break;
                case 3:
                    Console.Write("Enter filename to save goals: ");
                    string saveFileName = Console.ReadLine();
                    goalManager.SaveGoals(saveFileName);
                    break;
                case 4:
                    Console.Write("Enter filename to load goals: ");
                    string loadFileName = Console.ReadLine();
                    goalManager.LoadGoals(loadFileName);
                    break;
                case 5:
                    goalManager.RecordGoalEvent();
                    break;
                case 6:
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
