using ZooManagment.Domain.Models;

namespace ZooManagment.Domain.Interfaces.Services;

public interface ISpecieService
{
    /// <summary>
    /// Gets already existing specie or creates a new one and returns it
    /// </summary>
    /// <param name="specie"></param>
    /// <returns></returns>
    Task<Specie> GetOrCreateSpecieAsync(string specie);
}