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

}
