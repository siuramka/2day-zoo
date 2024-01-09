using ZooManagment.DataAccess.Repositories;
using ZooManagment.Domain.Models;

namespace ZooManagment.Business.Services.TransferTemplate;

public class EnclosureTransferVegetarian : EnclosureTransferTemplate
{
    private EnclosureRepository _enclosureRepository;
    
    public EnclosureTransferVegetarian(EnclosureRepository enclosureRepository) : base(enclosureRepository)
    {
        _enclosureRepository = enclosureRepository;
    }

    /// <summary>
    /// Gets a compatable enclosure by Vegetarian rules
    /// </summary>
    /// <param name="animal"></param>
    /// <returns></returns>
    protected override async Task<Enclosure?> GetEnclosureByRules(Animal animal)
    {
        return await _enclosureRepository.GetByFoodType(FoodType.Herbivore);
    }
}