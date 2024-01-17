using FluentAssertions;
using Moq;
using ZooManagment.Business.Services;
using ZooManagment.Domain.Dtos.Enclosure;
using ZooManagment.Domain.Interfaces.Repositories;
using ZooManagment.Domain.Models;

namespace ZooManagment.Test;

public class EnclosureServiceTests
{
    [Test]
    public async Task CreateAsync_ShouldCreateEnclosureWithLocationObject()
    {
        //Arrange
        var enclosureRepository = new Mock<IEnclosureRepository>();

        var locationObjectRepository = new Mock<ILocationObjectRepository>();

        var existingLocationObject = new LocationObject { Id = 1234, Name = "Tree" };
        locationObjectRepository.Setup(lor => lor.GetAsync(It.IsAny<string>())).ReturnsAsync(existingLocationObject);

        var enclosureService = new EnclosureService(enclosureRepository.Object, locationObjectRepository.Object);

        var enclosureCreateParsedDto = new EnclosureCreateParsedDto
        {
            Objects = new List<string> { existingLocationObject.Name },
            EnclosureSize = EnclosureSize.Large, LocationType = LocationType.Outside, Name = "Test Enclosure"
        };


        var expectedEnclosure = new Enclosure
        {
            Animals = new(), EnclosureSize = enclosureCreateParsedDto.EnclosureSize,
            Name = enclosureCreateParsedDto.Name, LocationType = enclosureCreateParsedDto.LocationType,
            EnclosureLocationObjects = new List<EnclosureLocationObject>()
                { new EnclosureLocationObject { LocationObjectId = existingLocationObject.Id } }
        };

        //Act
        Enclosure createdEnclosure = await enclosureService.CreateAsync(enclosureCreateParsedDto);

        //Exclude dynamically created IDs and empty Animals object
        createdEnclosure.Should().BeEquivalentTo(expectedEnclosure, options => options
            .Excluding(e => e.Id) 
            .Excluding(e => e.Animals)
            .Excluding(e => e.EnclosureLocationObjects[0].EnclosureId));
    }
    
    
}