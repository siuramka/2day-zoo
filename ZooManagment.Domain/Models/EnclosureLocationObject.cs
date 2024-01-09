namespace ZooManagment.Domain.Models;

public class EnclosureLocationObject
{
    public int EnclosureId { get; set; }
    public Enclosure Enclosure { get; set; }
    
    public int LocationObjectId { get; set; }
    public LocationObject LocationObject { get; set; }
}