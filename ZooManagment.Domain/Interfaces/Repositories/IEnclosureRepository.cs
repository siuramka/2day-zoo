using ZooManagment.Domain.Models;

namespace ZooManagment.Domain.Interfaces.Repositories;

public interface IEnclosureRepository
{
    Task CreateAsync(Enclosure enclosure);

    /// <summary>
    /// Get enclosure where every animal is the same specie
    /// </summary>
    /// <param name="specie"></param>
    /// <returns></returns>
    Task<Enclosure?> GetSameSpecie(string specie);

    /// <summary>
    /// Get enclosure where theres only 1 specie and all animals eat the same food.
    /// </summary>
    /// <param name="foodType"></param>
    /// <returns></returns>
    Task<Enclosure?> GetSameSpecieAndFoodType(FoodType foodType);

    /// <summary>
    /// Get enclosures where all animals eat the same food type
    /// </summary>
    /// <param name="foodType"></param>
    /// <returns></returns>
    Task<Enclosure?> GetByFoodType(FoodType foodType);

    /// <summary>
    /// Get first enclosure without any animals.
    /// </summary>
    /// <returns></returns>
    Task<Enclosure?> GetFirstEmpty();

    /// <summary>
    /// Get first enclosure that contains animals of single specie and all eat the food type
    /// </summary>
    /// <param name="foodType"></param>
    /// <param name="excludeEnclosureId">Enclosure id to exclude from search</param>
    /// <returns></returns>
    Task<Enclosure?> GetFirstWithOneSpeciesByFoodType(FoodType foodType, int excludeEnclosureId);

    /// <summary>
    /// Get first enclosure where all animals eat the same food type and 1 animal specie;
    /// </summary>
    /// <param name="foodType"></param>
    /// <returns></returns>
    Task<Enclosure?> GetFirstWithOneSpeciesByFoodType(FoodType foodType);

    /// <summary>
    /// Get first enclosure where all animals eat the same food and theres more than 1 animal species in the same enclosure
    /// </summary>
    /// <param name="foodType"></param>
    /// <param name="excludeEnclosureId">Enclosure id to exclude from search</param>
    /// <returns></returns>
    Task<Enclosure?> GetFirstWithMultipleSpeciesByFoodType(FoodType foodType, int excludeEnclosureId);

    /// <summary>
    /// Get first enclosure that contains two species and one of the species is the searching speacie.
    /// </summary>
    /// <param name="specie"></param>
    /// <returns></returns>
    Task<Enclosure?> GetFirstWithMultipleSpeciesBySpecie(string specie);
}