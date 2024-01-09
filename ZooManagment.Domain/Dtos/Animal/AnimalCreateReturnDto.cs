namespace ZooManagment.Domain.Dtos.Animal;

public class AnimalCreateReturnDto
{
    public int Id { get; set; }
    public string Species { get; set; } = "";
    public string Food { get; set; } = "";
    public string EnclosureName { get; set; } = "";
}