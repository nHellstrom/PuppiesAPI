namespace PuppiesAPI.Models;

public class Puppy
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; } = "Unknown Name";
    public string Breed { get; set; } = "Unknown Breed";
    public DateOnly BirthDate { get; set; } 
}