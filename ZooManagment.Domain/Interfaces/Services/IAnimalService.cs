using ZooManagment.Domain.Dtos.Animal;
using ZooManagment.Domain.Models;

namespace ZooManagment.Domain.Interfaces.Services;

public interface IAnimalService
{
    /// <summary>
    /// Create animal
    /// </summary>
    /// <param name="animalCreateParsedDto"></param>
    /// <returns></returns>
    Task<Animal> CreateAsync(AnimalCreateParsedDto animalCreateParsedDto);

    /// <summary>
    /// Deletes animal
    /// </summary>
    /// <param name="animalId"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(int animalId);
}