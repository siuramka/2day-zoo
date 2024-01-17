using ZooManagment.DataAccess.Repositories;
using ZooManagment.Domain.Dtos.Animal;
using ZooManagment.Domain.Interfaces.Repositories;
using ZooManagment.Domain.Interfaces.Services;
using ZooManagment.Domain.Models;

namespace ZooManagment.Business.Services;

public class AnimalService : IAnimalService
{
    private IAnimalRepository _animalRepository;
    private ISpecieService _specieService;

    public AnimalService(IAnimalRepository animalRepository, ISpecieService specieService)
    {
        _animalRepository = animalRepository;
        _specieService = specieService;
    }

    /// <summary>
    /// Create animal
    /// </summary>
    /// <param name="animalCreateParsedDto"></param>
    /// <returns></returns>
    public async Task<Animal> CreateAsync(AnimalCreateParsedDto animalCreateParsedDto)
    {
        var specie = await _specieService.GetOrCreateSpecieAsync(animalCreateParsedDto.Species);
        var animal = new Animal { FoodType = animalCreateParsedDto.Food, Specie = specie };

        await _animalRepository.CreateAsync(animal);

        return animal;
    }

    /// <summary>
    /// Deletes animal
    /// </summary>
    /// <param name="animalId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(int animalId)
    {
        var animal = await _animalRepository.GetByIdAsync(animalId);

        if (animal == null)
            return false;

        await _animalRepository.DeleteAsync(animal);

        return true;
    }
}