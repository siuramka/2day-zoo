using Moq;
using ZooManagment.Business.Services;
using ZooManagment.DataAccess.Repositories;
using ZooManagment.Domain.Interfaces;
using ZooManagment.Domain.Interfaces.Repositories;
using ZooManagment.Domain.Models;

namespace ZooManagment.Test;

public class SpecieServiceTests
{
    [Test]
    public async Task GetOrCreateSpecieAsync_ExistingSpecie_ReturnsExistingSpecie()
    {
        // Arrange
        var existingSpecieName = "Lion";
        var existingSpecie = new Specie { Id = 1, Name = existingSpecieName };

        var specieRepositoryMock = new Mock<ISpecieRepository>();
        specieRepositoryMock.Setup(repo => repo.GetAsync(existingSpecieName))
            .ReturnsAsync(existingSpecie);

        var specieService = new SpecieService(specieRepositoryMock.Object);

        // Act
        var result = await specieService.GetOrCreateSpecieAsync(existingSpecieName);

        // Assert
        Assert.AreEqual(existingSpecie.Name, result.Name);
    }
    
    [Test]
    public async Task GetOrCreateSpecieAsync_ExistingSpecie_ReturnsCreatedSpecie()
    {
        // Arrange
        var newSpecieName = "Lion";
        var newSpecie = new Specie { Id = 1, Name = newSpecieName };

        var specieRepositoryMock = new Mock<ISpecieRepository>();
        specieRepositoryMock.Setup(repo => repo.GetAsync(It.IsAny<string>()))
            .ReturnsAsync((Specie?)null);

        var specieService = new SpecieService(specieRepositoryMock.Object);

        // Act
        var result = await specieService.GetOrCreateSpecieAsync(newSpecieName);

        // Assert
        Assert.AreEqual(newSpecie.Name, result.Name);
    }
}