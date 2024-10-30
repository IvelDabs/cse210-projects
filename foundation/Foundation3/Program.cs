using System;
using System.Collections.Generic;

// Base class
public abstract class Activity
{
    private DateTime _date;
    private int _duration; // in minutes

    public Activity(DateTime date, int duration)
    {
        _date = date;
        _duration = duration;
    }

    public DateTime Date => _date;
    public int Duration => _duration;

    public abstract double GetDistance();
    public abstract double GetSpeed(); // Speed in miles per hour or kilometers per hour
    public abstract double GetPace(); // Pace in minutes per mile or minutes per kilometer

    public virtual string GetSummary()
    {
        return $"{Date:dd MMM yyyy} {GetType().Name} ({Duration} min) - " +
               $"Distance: {GetDistance():F1}, Speed: {GetSpeed():F1}, Pace: {GetPace():F1}";
    }
}

// Derived class for Running
public class Running : Activity
{
    private double _distance; // in miles

    public Running(DateTime date, int duration, double distance)
        : base(date, duration)
    {
        _distance = distance;
    }

    public override double GetDistance() => _distance;

    public override double GetSpeed() => (_distance / Duration) * 60; // mph

    public override double GetPace() => Duration / _distance; // min/mile
}

// Derived class for Cycling
public class Cycling : Activity
{
    private double _distance; // in miles
    private double _speed; // in mph

    public Cycling(DateTime date, int duration, double distance)
        : base(date, duration)
    {
        _distance = distance;
        _speed = GetSpeed();
    }

    public override double GetDistance() => _distance;

    public override double GetSpeed() => (_distance / Duration) * 60; // mph

    public override double GetPace() => Duration / _distance; // min/mile
}

// Derived class for Swimming
public class Swimming : Activity
{
    private int _laps;

    public Swimming(DateTime date, int duration, int laps)
        : base(date, duration)
    {
        _laps = laps;
    }

    public override double GetDistance() => (_laps * 50.0 / 1000.0); // in km

    public override double GetSpeed() => (GetDistance() / Duration) * 60; // kph

    public override double GetPace() => Duration / GetDistance(); // min/km
}

// Program to demonstrate the functionality
public class Program
{
    public static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 3.0),
            new Cycling(new DateTime(2022, 11, 3), 30, 10.0),
            new Swimming(new DateTime(2022, 11, 3), 30, 20)
        };

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
