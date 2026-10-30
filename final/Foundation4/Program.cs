using System;
using System.Collections.Generic;

// Uses miles / mph / min per mile throughout.

public class Activity
{
    // Encapsulation: all member variables are private.
    private DateTime _date;
    private int _minutes;

    public Activity(DateTime date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    public DateTime GetDate() => _date;
    public int GetMinutes() => _minutes;

    // Virtual methods: overridden in each derived class.
    public virtual double GetDistance() => 0;
    public virtual double GetSpeed() => 0;
    public virtual double GetPace() => 0;

    // Shared by all activities; uses the (overridden) methods above.
    public string GetSummary()
    {
        string name = GetType().Name; // Running, Cycling, Swimming
        return $"{_date:dd MMM yyyy} {name} ({_minutes} min): " +
               $"Distance {GetDistance():F1} miles, " +
               $"Speed: {GetSpeed():F1} mph, " +
               $"Pace: {GetPace():F1} min per mile";
    }
}

public class Running : Activity
{
    private double _distance; // miles

    public Running(DateTime date, int minutes, double distance)
        : base(date, minutes)
    {
        _distance = distance;
    }

    public override double GetDistance() => _distance;
    public override double GetSpeed() => (GetDistance() / GetMinutes()) * 60;
    public override double GetPace() => GetMinutes() / GetDistance();
}

public class Cycling : Activity
{
    private double _speed; // mph

    public Cycling(DateTime date, int minutes, double speed)
        : base(date, minutes)
    {
        _speed = speed;
    }

    public override double GetSpeed() => _speed;
    public override double GetDistance() => _speed * GetMinutes() / 60;
    public override double GetPace() => 60 / _speed;
}

public class Swimming : Activity
{
    private int _laps; // each lap is 50 meters

    public Swimming(DateTime date, int minutes, int laps)
        : base(date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistance() => _laps * 50 / 1000.0 * 0.62;
    public override double GetSpeed() => (GetDistance() / GetMinutes()) * 60;
    public override double GetPace() => GetMinutes() / GetDistance();
}

public class Program
{
    public static void Main()
    {
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 3.0),
            new Cycling(new DateTime(2022, 11, 4), 45, 12.0),
            new Swimming(new DateTime(2022, 11, 5), 40, 40)
        };

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}