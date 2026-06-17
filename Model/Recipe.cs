/// <summary>
/// Wrapper class for deserializing the recipes.json seed file.
/// </summary>
public class RecipeData
{
    public List<Recipe> Recipes { get; set; } = new();
}

/// <summary>
/// Represents a recipe with metadata, ingredients, instructions, and user reviews.
/// </summary>
public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Category { get; set; } = "";
    public string PrepTime { get; set; } = "";
    public string CookTime { get; set; } = "";
    public int Servings { get; set; }
    public List<Ingredient> Ingredients { get; set; } = new();
    public List<string> Instructions { get; set; } = new();
    public List<Review> Reviews { get; set; } = new();
}

/// <summary>
/// Represents a single ingredient with an amount, unit, and name.
/// </summary>
public class Ingredient
{
    public string Name { get; set; } = "";
    public double Amount { get; set; }
    public string Unit { get; set; } = "";
}

/// <summary>
/// Represents a user-submitted review with a star rating and comment.
/// </summary>
public class Review
{
    public int Rating { get; set; }
    public string Comment { get; set; } = "";
    public DateTime Date { get; set; } = DateTime.Now;
}
