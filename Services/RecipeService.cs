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

    /// <summary>
    /// Contains the last error message if an operation fails. Empty string if no error.
    /// </summary>
    public string ErrorMessage { get; private set; } = "";

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

        try
        {
            ErrorMessage = "";
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
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to load recipes: {ex.Message}";
            _recipes = new List<Recipe>();
        }

        return _recipes;
    }

    /// <summary>
    /// Adds a new recipe and assigns it a unique ID.
    /// </summary>
    public async Task<bool> AddRecipeAsync(Recipe recipe)
    {
        try
        {
            ErrorMessage = "";
            var recipes = await GetRecipesAsync();
            recipe.Id = recipes.Any() ? recipes.Max(r => r.Id) + 1 : 1;
            recipes.Add(recipe);
            await SaveAsync();
            return true;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to add recipe: {ex.Message}";
            return false;
        }
    }

    /// <summary>
    /// Updates an existing recipe by matching on ID.
    /// </summary>
    public async Task<bool> UpdateRecipeAsync(Recipe recipe)
    {
        try
        {
            ErrorMessage = "";
            var recipes = await GetRecipesAsync();
            var index = recipes.FindIndex(r => r.Id == recipe.Id);
            if (index >= 0)
            {
                recipes[index] = recipe;
                await SaveAsync();
            }
            return true;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to update recipe: {ex.Message}";
            return false;
        }
    }

    /// <summary>
    /// Deletes a recipe by ID.
    /// </summary>
    public async Task<bool> DeleteRecipeAsync(int id)
    {
        try
        {
            ErrorMessage = "";
            var recipes = await GetRecipesAsync();
            recipes.RemoveAll(r => r.Id == id);
            await SaveAsync();
            return true;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to delete recipe: {ex.Message}";
            return false;
        }
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
