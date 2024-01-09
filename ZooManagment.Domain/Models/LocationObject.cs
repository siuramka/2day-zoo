namespace ZooManagment.Domain.Models;

public class LocationObject
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    
    public List<EnclosureLocationObject> EnclosureLocationObjects { get; set; } = new();
}