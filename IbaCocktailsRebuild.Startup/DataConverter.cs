using Newtonsoft.Json.Linq;
using TestConsoleDotnet6.Startup.Models;

namespace TestConsoleDotnet6.Startup;

public class DataConverter
{
    public IList<Ingredient> ToIngredientsList(JToken input)
    {
        var ingredients = new List<Ingredient>();
        
        foreach (var child in input.Children<JProperty>())
        {
            var ingredientName = child.Name;
            
            var ingredient = child.Value.ToObject<Ingredient>();
            ingredient.Id = ingredients.Count + 1;
            ingredient.Name = ingredientName;

            ingredients.Add(ingredient);
        }

        return ingredients;
    }

    public IList<Recipe> ToRecipesList(JToken input, IList<Ingredient> ingredients)
    {
        var recipes = new List<Recipe>();
        
        foreach (var child in input.Children())
        {
            var recipe = ToRecipe(child, ingredients);
            recipe.Id = recipes.Count + 1;

            recipes.Add(recipe);
        }

        return recipes;
    }

    private Recipe ToRecipe(JToken input, IList<Ingredient> ingredients)
    {
        var recipe = input.ToObject<Recipe>();

        var ingredientsProperty = input.Children<JProperty>()
            .First(c => c.Name == "ingredients")
            .Value;

        var measuredIngredients = new List<MeasuredIngredient>();
        var specialIngredients = new List<SpecialIngredient>();

        foreach (var child in ingredientsProperty.Children())
        {
            var (measuredIngredient, specialIngredient) = DetermineIngredient(child, ingredients);

            if (measuredIngredient != null)
            {
                measuredIngredients.Add(measuredIngredient);
            }

            if (specialIngredient != null)
            {
                specialIngredients.Add(specialIngredient);
            }
        }

        recipe.Ingredients = measuredIngredients;
        recipe.SpecialIngredients = specialIngredients;

        return recipe;
    }

    private (MeasuredIngredient?, SpecialIngredient?) DetermineIngredient(JToken input, IList<Ingredient> ingredients)
    {
        var specialIngredientJToken = input.Children<JProperty>()
            .FirstOrDefault(c => c.Name == "special");

        if (specialIngredientJToken != null)
        {
            var specialIngredient = new SpecialIngredient
            {
                Description = specialIngredientJToken.Value.ToObject<string>()
            };

            return (null, specialIngredient);
        }

        var ingredientName = input.Children<JProperty>()
            .First(c => c.Name == "ingredient").Value
            .ToObject<string>();

        var ingredientId = ingredients.FirstOrDefault(i => i.Name == ingredientName)?.Id ?? -1;

        var measuredIngredient = input.ToObject<MeasuredIngredient>();
        measuredIngredient.IngredientId = ingredientId;

        return (measuredIngredient, null);
    }
}