namespace TestConsoleDotnet6.Startup.Models;

public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Glass { get; set; }
    public string Category { get; set; }
    public IEnumerable<MeasuredIngredient> Ingredients { get; set; }
    public IEnumerable<SpecialIngredient> SpecialIngredients { get; set; }
    public string? Garnish { get; set; }
    public string? Preparation { get; set; }
}