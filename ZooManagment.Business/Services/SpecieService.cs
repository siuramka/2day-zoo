using ZooManagment.DataAccess.Repositories;
using ZooManagment.Domain.Interfaces;
using ZooManagment.Domain.Interfaces.Repositories;
using ZooManagment.Domain.Interfaces.Services;
using ZooManagment.Domain.Models;

namespace ZooManagment.Business.Services;

public class SpecieService : ISpecieService
{
    private ISpecieRepository _specieRepository;

    public SpecieService(ISpecieRepository specieRepository)
    {
        _specieRepository = specieRepository;
    }

    /// <summary>
    /// Gets already existing specie or creates a new one and returns it
    /// </summary>
    /// <param name="specie"></param>
    /// <returns></returns>
    public async Task<Specie> GetOrCreateSpecieAsync(string specie)
    {
        var existingSpecie = await _specieRepository.GetAsync(specie);

        if (existingSpecie != null)
            return existingSpecie;

        var newSpecie = new Specie { Name = specie };

        await _specieRepository.CreateAsync(newSpecie);
        
        return newSpecie;
    }
}