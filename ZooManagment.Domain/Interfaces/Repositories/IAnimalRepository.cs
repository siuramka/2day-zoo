using ZooManagment.Domain.Models;

namespace ZooManagment.Domain.Interfaces.Repositories;

public interface IAnimalRepository
{
    Task DeleteAsync(Animal animal);
    Task CreateAsync(Animal animal);
    Task UpdateAsync(Animal animal);

    /// <summary>
    /// Get animals from enclosure
    /// </summary>
    /// <param name="enclosureId"></param>
    /// <returns></returns>
    Task<List<Animal>> GetFromEnclosureAsync(int enclosureId);

    /// <summary>
    /// Get animal by id
    /// </summary>
    /// <param name="enclosureId"></param>
    /// <returns></returns>
    Task<Animal?> GetByIdAsync(int enclosureId);
}