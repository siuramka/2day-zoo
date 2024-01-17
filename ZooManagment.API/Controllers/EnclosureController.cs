using Microsoft.AspNetCore.Mvc;
using ZooManagment.Business.Services;
using ZooManagment.Domain.Dtos.Enclosure;
using ZooManagment.Domain.Interfaces.Services;
using ZooManagment.Domain.Models;

namespace ZooManagment.API.Controllers;

[ApiController]
[Route("/api/enclosures")]
public class EnclosureController : ControllerBase
{
    private IEnclosureService _enclosureService;

    public EnclosureController(IEnclosureService enclosureService)
    {
        _enclosureService = enclosureService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMany(EnclosureListDto enclosures)
    {
        List<EnclosureDto> createdEnclosureDtos = new();

        foreach (var enclosureCreateDto in enclosures.Enclosures)
        {
            bool parsedLocation = Enum.TryParse(enclosureCreateDto.Location, out LocationType locationType);
            bool parsedSize = Enum.TryParse(enclosureCreateDto.Size, out EnclosureSize enclosureSize);
        
            if (!parsedLocation || !parsedSize)
                return BadRequest("Failed to parse data");
        
            var parsedEnclosure = new EnclosureCreateParsedDto
            {
                EnclosureSize = enclosureSize, LocationType = locationType, Name = enclosureCreateDto.Name,
                Objects = enclosureCreateDto.Objects
            };
        
            var enclosure = await _enclosureService.CreateAsync(parsedEnclosure);
            createdEnclosureDtos.Add(new EnclosureDto
            {
                Id = enclosure.Id, Location = enclosure.LocationType.ToString(), Name = enclosure.Name,
                Size = enclosure.EnclosureSize.ToString(),
                Objects = enclosure.EnclosureLocationObjects.Select(elo => elo.LocationObject.Name).ToList()
            });
        }

        return Ok(createdEnclosureDtos);
    }
}