using System;

public class Tamagotchi
{
    public int Id                      { get; set; }
    public string Name                 { get; set; }
    public DateTime Birthday           { get; set; }
    public int HungerLevel             { get; set; }
    public int HappinessLevel          { get; set; }
    public DateTime LastInteractedWith { get; set; }
    public bool IsDead
    {
        get
        {
            return ((DateTime.Now - LastInteractedWith).TotalDays > 3);
        }
    }

    public Tamagotchi(string name)
    {
        var dateTime = DateTime.Now;

        Name = name;
        Birthday = dateTime;
        HungerLevel = 0;
        HappinessLevel = 0;
        LastInteractedWith = dateTime;
    }
}

