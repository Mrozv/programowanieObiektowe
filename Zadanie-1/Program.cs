using System;

public class Person
{
    private string firstName;
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; } = null;
    public DateTime? DateOfDeath { get; set; } = null;

    public Person(string fullName)
    {
        FullName = fullName;
    }

    public string FirstName
    {
        get => firstName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Imie nie moze byc puste.");
            firstName = value;
        }
    }

    public string FullName
    {
        get => $"{FirstName} {LastName}";
        set
        {
            var parts = value.Split(' ');
            if (parts.Length == 0)
                throw new ArgumentException("Podano niewlasciwe imie i nazwisko.");

            FirstName = parts[0];
            LastName = parts.Length > 1 ? string.Join(' ', parts[1..]) : string.Empty;
        }
    }

    public TimeSpan? Age
    {
        get
        {
            if (!DateOfBirth.HasValue)
                return null;

            DateTime dateOfDeath = DateOfDeath ?? DateTime.Now;
            return dateOfDeath - DateOfBirth.Value;
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Podaj imie i nazwisko:");
        string fullName = Console.ReadLine();

        Person person = new(fullName);

        Console.WriteLine("Podaj date urodzenia (RRRR-MM-DD):");
        string dateOfBirthInput = Console.ReadLine();
        if (DateTime.TryParse(dateOfBirthInput, out DateTime dateOfBirth))
        {
            person.DateOfBirth = dateOfBirth;
        }

        Console.WriteLine($"Imie: {person.FirstName}");
        Console.WriteLine($"Nazwisko: {person.LastName}");
        Console.WriteLine($"Imie i Nazwisko: {person.FullName}");
        Console.WriteLine($"Wiek: {(person.Age.HasValue ? person.Age.Value.TotalDays / 365.25 : (double?)null):F2} lat");
    }
}
