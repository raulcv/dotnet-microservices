namespace GetChild.Models
{
  public class Child
  {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public int BirthYear { get; set; }
    public string? ImageUrl { get; set; }
  }
}