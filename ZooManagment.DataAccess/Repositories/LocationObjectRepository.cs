using Microsoft.EntityFrameworkCore;
using ZooManagment.Domain.Models;

namespace ZooManagment.DataAccess.Repositories;

public class LocationObjectRepository
{
    private readonly ZooDbContext _dbContext;

    public LocationObjectRepository(ZooDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<LocationObject?> GetAsync(string locationObjectName)
    {
        return await _dbContext.LocationObjects.FirstOrDefaultAsync(lo => lo.Name.Equals(locationObjectName));
    }

    public async Task CreateAsync(LocationObject locationObject)
    {
        await _dbContext.AddAsync(locationObject);
        await _dbContext.SaveChangesAsync();
    }
}