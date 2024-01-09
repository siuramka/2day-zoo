using ZooManagment.Business.Services.TransferTemplate;
using ZooManagment.DataAccess;
using ZooManagment.DataAccess.Repositories;
using ZooManagment.Domain.Models;

namespace ZooManagment.Business.Services;

public class TransferService
{
    private EnclosureRepository _enclosureRepository;
    private AnimalRepository _animalRepository;
    private ZooDbContext _dbContext;

    public TransferService(EnclosureRepository enclosureRepository, AnimalRepository animalRepository,
        ZooDbContext dbContext)
    {
        _enclosureRepository = enclosureRepository;
        _animalRepository = animalRepository;
        _dbContext = dbContext;
    }

    /// <summary>
    /// Transfer animal to a compatible enclosure
    /// </summary>
    /// <param name="animal">Animal entity with tracking</param>
    /// <returns>Enclosure of which animal was put in</returns>
    public async Task<Enclosure?> TransferAsync(Animal animal)
    {
        EnclosureTransferTemplate transferTemplate = GetTransferTemplate(animal);

        Enclosure? enclosure = await transferTemplate.GetEnclosureAsync(animal);

        if (enclosure == null)
        {
            await MoveHerbivoresToCompatibleEnclosure();
            enclosure = await transferTemplate.GetEnclosureAsync(animal);
        }


        //assign enclosure
        animal.EnclosureId = enclosure.Id;

        await _animalRepository.UpdateAsync(animal);

        return enclosure;
    }

    /// <summary>
    /// Moves vegetarian animals who are single specie from single enclosure to a different enclosure with other vegetarian animals so we get a new empty enclosure
    /// </summary>
    private async Task MoveHerbivoresToCompatibleEnclosure()
    {
        var sameSpecieAndFoodTypeEnclosure = await _enclosureRepository.GetSameSpecieAndFoodType(FoodType.Herbivore);
        var animalsInEnclosure = await _animalRepository.GetFromEnclosureAsync(sameSpecieAndFoodTypeEnclosure.Id);

        var vegetarianEnclosureSingleSpecie =
            await _enclosureRepository.GetFirstWithOneSpeciesByFoodType(FoodType.Herbivore,
                sameSpecieAndFoodTypeEnclosure.Id);

        var vegetarianEnclosureMultipleSpecie =
            await _enclosureRepository.GetFirstWithMultipleSpeciesByFoodType(FoodType.Herbivore,
                sameSpecieAndFoodTypeEnclosure.Id);

        var newEnclosure = vegetarianEnclosureSingleSpecie ?? vegetarianEnclosureMultipleSpecie;

        foreach (var animal in animalsInEnclosure)
        {
            animal.EnclosureId = newEnclosure.Id;
            animal.Enclosure = newEnclosure;
            await _dbContext.SaveChangesAsync();
        }
    }


    private EnclosureTransferTemplate GetTransferTemplate(Animal animal)
    {
        switch (animal.FoodType)
        {
            case FoodType.Herbivore:
                return new EnclosureTransferVegetarian(_enclosureRepository);
            case FoodType.Carnivore:
                return new EnclosureTransferMeatEater(_enclosureRepository);
            default:
                throw new Exception("Invalid food type");
        }
    }
}