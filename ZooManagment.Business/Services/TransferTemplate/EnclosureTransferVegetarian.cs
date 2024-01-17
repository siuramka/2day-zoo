using ZooManagment.DataAccess.Repositories;
using ZooManagment.Domain.Interfaces.Repositories;
using ZooManagment.Domain.Models;

namespace ZooManagment.Business.Services.TransferTemplate;

public class EnclosureTransferVegetarian : EnclosureTransferTemplate
{
    private IEnclosureRepository _enclosureRepository;
    
    public EnclosureTransferVegetarian(IEnclosureRepository enclosureRepository) : base(enclosureRepository)
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