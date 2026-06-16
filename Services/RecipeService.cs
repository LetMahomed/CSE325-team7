using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Text.Json;

namespace CSE325_team7.Services;

/// <summary>
/// Service responsible for managing recipes with localStorage persistence.
/// On first load, seeds data from a static JSON file. All subsequent reads/writes
/// use browser localStorage for offline-capable, account-free storage.
/// </summary>
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

    /// <summary>
    /// Retrieves all recipes. Loads from localStorage if available,
    /// otherwise seeds from the static recipes.json file.
    /// </summary>
    public async Task<List<Recipe>> GetRecipesAsync()
    {
        if (_recipes != null)
            return _recipes;

        // Try loading from localStorage first
        var json = await _js.InvokeAsync<string?>("localStorage.getItem", StorageKey);

        if (!string.IsNullOrWhiteSpace(json))
        {
            _recipes = JsonSerializer.Deserialize<List<Recipe>>(json) ?? new List<Recipe>();
        }
        else
        {
            // First visit: seed from static JSON file
            var data = await _http.GetFromJsonAsync<RecipeData>("data/recipes.json");
            _recipes = data?.Recipes ?? new List<Recipe>();
            await SaveAsync();
        }

        return _recipes;
    }

    /// <summary>
    /// Adds a new recipe and assigns it a unique ID.
    /// </summary>
    public async Task AddRecipeAsync(Recipe recipe)
    {
        var recipes = await GetRecipesAsync();
        // Auto-increment ID based on existing max
        recipe.Id = recipes.Any() ? recipes.Max(r => r.Id) + 1 : 1;
        recipes.Add(recipe);
        await SaveAsync();
    }

    /// <summary>
    /// Updates an existing recipe by matching on ID.
    /// </summary>
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

    /// <summary>
    /// Deletes a recipe by ID.
    /// </summary>
    public async Task DeleteRecipeAsync(int id)
    {
        var recipes = await GetRecipesAsync();
        recipes.RemoveAll(r => r.Id == id);
        await SaveAsync();
    }

    /// <summary>
    /// Persists the current recipe list to browser localStorage.
    /// </summary>
    private async Task SaveAsync()
    {
        var json = JsonSerializer.Serialize(_recipes);
        await _js.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
    }
}
