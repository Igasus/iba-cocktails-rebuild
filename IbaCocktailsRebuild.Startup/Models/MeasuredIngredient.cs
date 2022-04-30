namespace TestConsoleDotnet6.Startup.Models;

public class MeasuredIngredient
{
    public int IngredientId { get; set; }
    public string Unit { get; set; }
    public decimal Amount { get; set; }
    public string? Label { get; set; }
}