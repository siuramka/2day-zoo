namespace ZooManagment.Domain.Models;

public class Enclosure
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public EnclosureSize EnclosureSize { get; set; }
    public LocationType LocationType { get; set; }

    public List<EnclosureLocationObject> EnclosureLocationObjects { get; set; } = new();
    public List<Animal> Animals { get; set; }
}