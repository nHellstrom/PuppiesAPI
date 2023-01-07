namespace PuppiesAPI.Models;


public class Puppy
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string? Name { get; set; }
    public string? Breed { get; set; }
    public DateOnly? BirthDate { get; set; } 
}