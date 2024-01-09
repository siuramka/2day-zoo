using ZooManagment.Domain.Models;

namespace ZooManagment.Domain.Dtos.Animal;

public class AnimalCreateParsedDto
{
    public string Species { get; set; } = "";
    public FoodType Food { get; set; }
}