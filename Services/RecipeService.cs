using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Text.Json;

namespace CSE325_team7.Services;

public class RecipeService
{
    private readonly IJSRuntime _js;
    private readonly HttpClient _http;
    private const string StorageKey = "recipes";
    private List<Recipe>? _recipes;

    public RecipeService(IJSRuntime js, HttpClient http)
    {
        _js = js;
        _http = http;
    }

    public async Task<List<Recipe>> GetRecipesAsync()
    {
        if (_recipes != null)
            return _recipes;

        var json = await _js.InvokeAsync<string?>("localStorage.getItem", StorageKey);

        if (!string.IsNullOrWhiteSpace(json))
        {
            _recipes = JsonSerializer.Deserialize<List<Recipe>>(json) ?? new List<Recipe>();
        }
        else
        {
            var data = await _http.GetFromJsonAsync<RecipeData>("data/recipes.json");
            _recipes = data?.Recipes ?? new List<Recipe>();
            await SaveAsync();
        }

        return _recipes;
    }

    public async Task AddRecipeAsync(Recipe recipe)
    {
        var recipes = await GetRecipesAsync();
        recipe.Id = recipes.Any() ? recipes.Max(r => r.Id) + 1 : 1;
        recipes.Add(recipe);
        await SaveAsync();
    }

    public async Task UpdateRecipeAsync(Recipe recipe)
    {
        var recipes = await GetRecipesAsync();
        var index = recipes.FindIndex(r => r.Id == recipe.Id);
        if (index >= 0)
        {
            recipes[index] = recipe;
            await SaveAsync();
        }
    }

    public async Task DeleteRecipeAsync(int id)
    {
        var recipes = await GetRecipesAsync();
        recipes.RemoveAll(r => r.Id == id);
        await SaveAsync();
    }

    private async Task SaveAsync()
    {
        var json = JsonSerializer.Serialize(_recipes);
        await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
    }
}
