using FluentAssertions;
using Moq;
using ZooManagment.Business.Services;
using ZooManagment.Domain.Dtos.Animal;
using ZooManagment.Domain.Interfaces.Repositories;
using ZooManagment.Domain.Interfaces.Services;
using ZooManagment.Domain.Models;

namespace ZooManagment.Test;

public class AnimalServiceTests
{
    [Test]
    public async Task CreateAsync_ShouldCreateNewAnimal()
    {
        //Arrange
        var animalRepository = new Mock<IAnimalRepository>();
        var specieService = new Mock<ISpecieService>();

        string spiecieName = "Lion";

        var specie = new Specie { Id = 1, Name = spiecieName };

        specieService.Setup(ss => ss.GetOrCreateSpecieAsync(It.IsAny<string>()))
            .ReturnsAsync(specie);

        var parsedAnimalDto = new AnimalCreateParsedDto { Food = FoodType.Carnivore, Species = spiecieName };

        var animalService = new AnimalService(animalRepository.Object, specieService.Object);
        
        var expectedAnimal = new Animal { FoodType = FoodType.Carnivore, Specie = specie };
        
        //Act
        var resultAnimal = await animalService.CreateAsync(parsedAnimalDto);

        //Assert
        resultAnimal.Should().BeEquivalentTo(expectedAnimal);
    }

    [Test]
    public async Task DeleteAsync_ShouldDelete_ExistingAnimal_ById()
    {
        //Arrange
        var animalRepository = new Mock<IAnimalRepository>();

        var animal = new Animal { Id = 1 };
        animalRepository.Setup(ar => ar.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(animal);
        
        var specieService = new Mock<ISpecieService>();
        
        var animalService = new AnimalService(animalRepository.Object, specieService.Object);
        //Act
        var isDeleted = await animalService.DeleteAsync(It.IsAny<int>());

        //Assert
        Assert.IsTrue(isDeleted);
    }
    
    [Test]
    public async Task DeleteAsync_ShouldNotDelete_NonExistingAnimal_ById()
    {
        //Arrange
        var animalRepository = new Mock<IAnimalRepository>();

        animalRepository.Setup(ar => ar.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Animal?)null);
        
        var specieService = new Mock<ISpecieService>();
        
        var animalService = new AnimalService(animalRepository.Object, specieService.Object);
        //Act
        var isDeleted = await animalService.DeleteAsync(It.IsAny<int>());

        //Assert
        Assert.IsFalse(isDeleted);
    }
}