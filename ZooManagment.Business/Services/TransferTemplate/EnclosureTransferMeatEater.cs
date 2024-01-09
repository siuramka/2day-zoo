using ZooManagment.DataAccess.Repositories;
using ZooManagment.Domain.Models;

namespace ZooManagment.Business.Services.TransferTemplate;

public class EnclosureTransferMeatEater : EnclosureTransferTemplate
{
    private EnclosureRepository _enclosureRepository;

    public EnclosureTransferMeatEater(EnclosureRepository enclosureRepository) : base(enclosureRepository)
    {
        _enclosureRepository = enclosureRepository;
    }

    /// <summary>
    /// Gets the compatible enclosure by Meat Eater rules
    /// </summary>
    /// <param name="animal"></param>
    /// <returns>e</returns>
    protected override async Task<Enclosure?> GetEnclosureByRules(Animal animal)
    {
        var meatEatingEnclosureWithTwoSpeciesIncludingThisSpecie =
            await _enclosureRepository.GetFirstWithMultipleSpeciesBySpecie(animal.Specie.Name);
            
        if (meatEatingEnclosureWithTwoSpeciesIncludingThisSpecie != null)
            return meatEatingEnclosureWithTwoSpeciesIncludingThisSpecie;

        var meatEatingSingleSpecieEnclosure =
            await _enclosureRepository.GetFirstWithOneSpeciesByFoodType(FoodType.Carnivore);
        
        return meatEatingSingleSpecieEnclosure;
    }
}