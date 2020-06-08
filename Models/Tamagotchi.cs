using System;

public class Tamagotchi
{
    public int Id             { get; set; }
    public string Name        { get; set; }
    public DateTime Birthday  { get; set; }
    public int HungerLevel    { get; set; }
    public int HappinessLevel { get; set; }

    public Tamagotchi(string name)
    {
        Name = name;
        Birthday = DateTime.Now;
        HungerLevel = 0;
        HappinessLevel = 0;
    }

    public void PlayTimes()
    {
        HappinessLevel += 5;
        HungerLevel += 3;
    }

    public void Feedings()
    {
        HungerLevel -= 5;
        HappinessLevel -= 3;
    }
}
