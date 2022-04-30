using System.Text.Json;
using Newtonsoft.Json.Linq;
using TestConsoleDotnet6.Startup;

var dataConverter = new DataConverter();

var inputDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "../../../wwwroot", "input");
var outputDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "../../../wwwroot", "output");

var ingredientsInputJson = JToken.Parse(File.ReadAllText(Path.Combine(inputDirectoryPath, "ingredients.json")));
var recipesInputJson = JToken.Parse(File.ReadAllText(Path.Combine(inputDirectoryPath, "recipes.json")));

var ingredients = dataConverter.ToIngredientsList(ingredientsInputJson);
var cocktails = dataConverter.ToRecipesList(recipesInputJson, ingredients);

var jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true };
var ingredientsJsonText = JsonSerializer.Serialize(ingredients, jsonSerializerOptions);
var cocktailsJsonText = JsonSerializer.Serialize(cocktails, jsonSerializerOptions);

File.WriteAllText(Path.Combine(outputDirectoryPath, "ingredients.json"), ingredientsJsonText);
File.WriteAllText(Path.Combine(outputDirectoryPath, "recipes.json"), cocktailsJsonText);
