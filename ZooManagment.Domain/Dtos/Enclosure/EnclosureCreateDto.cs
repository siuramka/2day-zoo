namespace ZooManagment.Domain.Dtos.Enclosure;

public class EnclosureCreateDto
{
    public string Name { get; set; } = "";
    public string Size { get; set; } = "";
    public string Location { get; set; } = "";
    public List<string> Objects { get; set; } = new();
}