using ZooManagment.Domain.Models;

namespace ZooManagment.Domain.Interfaces.Repositories;

public interface ISpecieRepository
{
    Task<Specie?> GetAsync(string specie);
    Task CreateAsync(Specie specie);
}