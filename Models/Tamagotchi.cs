using System;

public class Tamagotchi
{
    public int Id             { get; set; }
    public string Name        { get; set; }
    public DateTime Birthday  { get; set; }
    public HungerLevel int    { get; set; }
    public HappinessLevel int { get; set; }
}
