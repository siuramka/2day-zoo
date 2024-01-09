using Microsoft.AspNetCore.Mvc;
using ZooManagment.Business.Services;
using ZooManagment.DataAccess.Repositories;
using ZooManagment.Domain.Dtos.Animal;
using ZooManagment.Domain.Models;

namespace ZooManagment.API.Controllers;

[ApiController]
[Route("/api/animals")]
public class AnimalController : ControllerBase
{
    private AnimalService _animalService;
    private TransferService _transferService;

    public AnimalController(AnimalService animalService, TransferService transferService)
    {
        _animalService = animalService;
        _transferService = transferService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMany(AnimalCreateListDto animals)
    {
        List<AnimalCreateReturnDto> returnDtos = new();

        foreach (var animalCreateDto in animals.Animals)
        {
            bool isFoodTypeParsed = Enum.TryParse(animalCreateDto.Food, out FoodType foodType);
            if (!isFoodTypeParsed)
                return BadRequest();

            for (var i = 0; i < animalCreateDto.Amount; i++)
            {
                var animalParsedDto = new AnimalCreateParsedDto { Species = animalCreateDto.Species, Food = foodType };
                var createdAnimal = await _animalService.CreateAsync(animalParsedDto);

                var enclosure = await _transferService.TransferAsync(createdAnimal);
                if (enclosure != null)
                {
                    returnDtos.Add(new AnimalCreateReturnDto
                    {
                        EnclosureName = enclosure.Name, Food = createdAnimal.FoodType.ToString(),
                        Species = createdAnimal.Specie.Name, Id = createdAnimal.Id
                    });
                }
            }
        }

        return Ok(returnDtos);
    }

    [HttpDelete]
    [Route("/{animalId}")]
    public async Task<IActionResult> Delete(int animalId)
    {
        var isDeleted = await _animalService.DeleteAsync(animalId);

        if (isDeleted)
            return NoContent();

        return BadRequest();
    }
}