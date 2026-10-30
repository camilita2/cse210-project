using System;

public class Address
{
    private string _street;
    private string _city;
    private string _state;
    private string _country;

    public Address(string street, string city, string state, string country)
    {
        _street = street;
        _city = city;
        _state = state;
        _country = country;
    }

    public override string ToString()
    {
        return $"{_street}\n{_city}, {_state}, {_country}";
    }
}

public class Event
{
    private string _title;
    private string _description;
    private string _date;
    private string _time;
    private Address _address;
    private string _eventType;

    public Event(string eventType, string title, string description,
                 string date, string time, Address address)
    {
        _eventType = eventType;
        _title = title;
        _description = description;
        _date = date;
        _time = time;
        _address = address;
    }

    public string GetStandardDetails()
    {
        return $"Title: {_title}\n" +
               $"Description: {_description}\n" +
               $"Date: {_date}\n" +
               $"Time: {_time}\n" +
               $"Address:\n{_address}";
    }

    // Derived classes override this to add their own details.
    public virtual string GetFullDetails()
    {
        return $"Event Type: {_eventType}\n{GetStandardDetails()}";
    }

    public string GetShortDescription()
    {
        return $"{_eventType}: {_title} on {_date}";
    }
}

public class Lecture : Event
{
    private string _speaker;
    private int _capacity;

    public Lecture(string title, string description, string date, string time,
                   Address address, string speaker, int capacity)
        : base("Lecture", title, description, date, time, address)
    {
        _speaker = speaker;
        _capacity = capacity;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nSpeaker: {_speaker}\nCapacity: {_capacity}";
    }
}

public class Reception : Event
{
    private string _rsvpEmail;

    public Reception(string title, string description, string date, string time,
                     Address address, string rsvpEmail)
        : base("Reception", title, description, date, time, address)
    {
        _rsvpEmail = rsvpEmail;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nRSVP Email: {_rsvpEmail}";
    }
}

public class OutdoorGathering : Event
{
    private string _weather;

    public OutdoorGathering(string title, string description, string date, string time,
                            Address address, string weather)
        : base("Outdoor Gathering", title, description, date, time, address)
    {
        _weather = weather;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nWeather Forecast: {_weather}";
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Address a1 = new Address("525 S Center St", "Rexburg", "ID", "USA");
        Address a2 = new Address("1 Rue de Rivoli", "Paris", "Ile-de-France", "France");
        Address a3 = new Address("200 Riverside Park Dr", "Idaho Falls", "ID", "USA");

        Event lecture = new Lecture(
            "The Future of Renewable Energy",
            "A talk on new advances in solar and wind technology.",
            "November 15, 2026", "7:00 PM", a1, "Dr. Emily Carter", 150);

        Event reception = new Reception(
            "Annual Gala Reception",
            "An evening of dinner, music, and networking.",
            "December 5, 2026", "6:30 PM", a2, "rsvp@eventsplus.com");

        Event outdoor = new OutdoorGathering(
            "Community Picnic",
            "Food, games, and fun for the whole family.",
            "June 20, 2027", "12:00 PM", a3, "Sunny with a high of 78F");

        Event[] events = { lecture, reception, outdoor };

        foreach (Event e in events)
        {
            Console.WriteLine("=== Standard Details ===");
            Console.WriteLine(e.GetStandardDetails());
            Console.WriteLine();
            Console.WriteLine("=== Full Details ===");
            Console.WriteLine(e.GetFullDetails());
            Console.WriteLine();
            Console.WriteLine("=== Short Description ===");
            Console.WriteLine(e.GetShortDescription());
            Console.WriteLine("\n------------------------------\n");
        }
    }
}