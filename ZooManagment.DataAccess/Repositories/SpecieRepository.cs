using Microsoft.EntityFrameworkCore;
using ZooManagment.Domain.Interfaces.Repositories;
using ZooManagment.Domain.Models;

namespace ZooManagment.DataAccess.Repositories;

public class SpecieRepository : ISpecieRepository
{
    private readonly ZooDbContext _dbContext;

    public SpecieRepository(ZooDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Specie?> GetAsync(string specie)
    {
        return await _dbContext.Species.FirstOrDefaultAsync(s => s.Name.Equals(specie));
    }
    
    public async Task CreateAsync(Specie specie)
    {
        await _dbContext.AddAsync(specie);
        await _dbContext.SaveChangesAsync();
    }
}