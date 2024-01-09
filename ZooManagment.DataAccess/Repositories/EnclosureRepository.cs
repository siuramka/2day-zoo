using Microsoft.EntityFrameworkCore;
using ZooManagment.Domain.Models;

namespace ZooManagment.DataAccess.Repositories;

public class EnclosureRepository
{
    private readonly ZooDbContext _dbContext;

    public EnclosureRepository(ZooDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(Enclosure enclosure)
    {
        await _dbContext.AddAsync(enclosure);
        await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Get enclosure where every animal is the same specie
    /// </summary>
    /// <param name="specie"></param>
    /// <returns></returns>
    public async Task<Enclosure?> GetSameSpecie(string specie)
    {
        return await _dbContext.Enclosures.Include(e => e.Animals)
            .FirstOrDefaultAsync(e => e.Animals.All(a => a.Specie.Name.Equals(specie)));
    }

    /// <summary>
    /// Get enclosure where theres only 1 specie and all animals eat the same food.
    /// </summary>
    /// <param name="foodType"></param>
    /// <returns></returns>
    public async Task<Enclosure?> GetSameSpecieAndFoodType(FoodType foodType)
    {
        return await _dbContext.Enclosures.Include(e => e.Animals)
            .FirstOrDefaultAsync(e =>
                e.Animals.Select(a => a.Specie.Name).Distinct().Count() == 1 &&
                e.Animals.All(a => a.FoodType.Equals(foodType)));
    }

    /// <summary>
    /// Get enclosures where all animals eat the same food type
    /// </summary>
    /// <param name="foodType"></param>
    /// <returns></returns>
    public async Task<Enclosure?> GetByFoodType(FoodType foodType)
    {
        return await _dbContext.Enclosures.Include(e => e.Animals)
            .FirstOrDefaultAsync(e => e.Animals.All(a => a.FoodType.Equals(foodType)));
    }

    /// <summary>
    /// Get first enclosure without any animals.
    /// </summary>
    /// <returns></returns>
    public async Task<Enclosure?> GetFirstEmpty()
    {
        return await _dbContext.Enclosures.Include(e => e.Animals).FirstOrDefaultAsync(e => e.Animals.Count == 0);
    }

    /// <summary>
    /// Get first enclosure that contains animals of single specie and all eat the food type
    /// </summary>
    /// <param name="foodType"></param>
    /// <param name="excludeEnclosureId">Enclosure id to exclude from search</param>
    /// <returns></returns>
    public async Task<Enclosure?> GetFirstWithOneSpeciesByFoodType(FoodType foodType, int excludeEnclosureId)
    {
        return await _dbContext.Enclosures.Include(e => e.Animals).ThenInclude(a => a.Specie)
            .FirstOrDefaultAsync(e => e.Id != excludeEnclosureId && e.Animals.All(a => a.FoodType == foodType)
                                                                 && e.Animals.Select(a => a.Specie.Name).Distinct()
                                                                     .Count() == 1);
    }

    /// <summary>
    /// Get first enclosure where all animals eat the same food type and 1 animal specie;
    /// </summary>
    /// <param name="foodType"></param>
    /// <returns></returns>
    public async Task<Enclosure?> GetFirstWithOneSpeciesByFoodType(FoodType foodType)
    {
        return await _dbContext.Enclosures.Include(e => e.Animals).ThenInclude(a => a.Specie)
            .FirstOrDefaultAsync(e => e.Animals.All(a => a.FoodType == foodType)
                                      && e.Animals.Select(a => a.Specie.Name).Distinct().Count() == 1);
    }
    
    /// <summary>
    /// Get first enclosure where all animals eat the same food and theres more than 1 animal species in the same enclosure
    /// </summary>
    /// <param name="foodType"></param>
    /// <param name="excludeEnclosureId">Enclosure id to exclude from search</param>
    /// <returns></returns>
    public async Task<Enclosure?> GetFirstWithMultipleSpeciesByFoodType(FoodType foodType, int excludeEnclosureId)
    {
        return await _dbContext.Enclosures.Include(e => e.Animals).ThenInclude(a => a.Specie)
            .FirstOrDefaultAsync(e => e.Id != excludeEnclosureId && e.Animals.All(a => a.FoodType == foodType)
                                                                 && e.Animals.Select(a => a.Specie.Name).Distinct()
                                                                     .Count() > 1);
    }

    /// <summary>
    /// Get first enclosure that contains two species and one of the species is the searching speacie.
    /// </summary>
    /// <param name="specie"></param>
    /// <returns></returns>
    public async Task<Enclosure?> GetFirstWithMultipleSpeciesBySpecie(string specie)
    {
        return await _dbContext.Enclosures.Include(e => e.Animals).ThenInclude(a => a.Specie)
            .FirstOrDefaultAsync(e =>
                e.Animals.Select(a => a.Specie.Name).Distinct().Count() == 2 &&
                e.Animals.Any(a => a.Specie.Name.Equals(specie)));
    }
}