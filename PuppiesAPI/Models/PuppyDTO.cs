namespace PuppiesAPI.Models;

public interface IPuppyDTO
{
    string? Name { get; }
    string? Breed { get; }
    string? BirthDate { get; }
}

public class PuppyDTO : IPuppyDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Breed { get; set; }
    public string? BirthDate { get; set; }
}

public class PuppyDTOnoID : IPuppyDTO
{
    public string? Name { get; set; }
    public string? Breed { get; set; }
    public string? BirthDate { get; set; }
}