namespace ZooManagment.Domain.Dtos.Animal;

public class AnimalCreateDto
{
    public string Species { get; set; } = "";
    public string Food { get; set; } = "";
    public int Amount { get; set; }
}