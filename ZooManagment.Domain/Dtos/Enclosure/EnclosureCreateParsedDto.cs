using ZooManagment.Domain.Models;

namespace ZooManagment.Domain.Dtos.Enclosure;

public class EnclosureCreateParsedDto
{
    public string Name { get; set; } = "";
    public EnclosureSize EnclosureSize { get; set; }
    public LocationType LocationType { get; set; }
    public List<string> Objects { get; set; }
}