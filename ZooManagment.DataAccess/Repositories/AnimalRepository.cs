using Microsoft.EntityFrameworkCore;
using ZooManagment.Domain.Models;

namespace ZooManagment.DataAccess.Repositories;

public class AnimalRepository
{
    private readonly ZooDbContext _dbContext;

    public AnimalRepository(ZooDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task DeleteAsync(Animal animal)
    {
        _dbContext.Remove(animal);
        await _dbContext.SaveChangesAsync();
    }
    public async Task CreateAsync(Animal animal)
    {
        await _dbContext.AddAsync(animal);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Animal animal)
    {
        _dbContext.Update(animal);
        await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Get animals from enclosure
    /// </summary>
    /// <param name="enclosureId"></param>
    /// <returns></returns>
    public async Task<List<Animal>> GetFromEnclosureAsync(int enclosureId)
    {
        return await _dbContext.Animals.Where(a => a.EnclosureId == enclosureId).ToListAsync();
    }
    
    /// <summary>
    /// Get animal by id
    /// </summary>
    /// <param name="enclosureId"></param>
    /// <returns></returns>
    public async Task<Animal?> GetByIdAsync(int enclosureId)
    {
        return await _dbContext.Animals.FirstOrDefaultAsync(a => a.Id == enclosureId);
    }

}