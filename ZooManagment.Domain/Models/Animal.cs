namespace ZooManagment.Domain.Models;

public class Animal
{
    public int Id { get; set; }
    public FoodType FoodType { get; set; } // each food type should be flexible for animal and not specific to specie
    
    public int SpecieId { get; set; }
    public Specie Specie { get; set; }

    public int? EnclosureId { get; set; } = null;
    public Enclosure Enclosure { get; set; }
}