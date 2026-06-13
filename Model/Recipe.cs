public class RecipeData
{
    public List<Recipe> Recipes { get; set; } = new();
}

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

public class Ingredient
{
    public string Name { get; set; } = "";
    public double Amount { get; set; }
    public string Unit { get; set; } = "";
}

public class Review
{
    public int Rating { get; set; }
    public string Comment { get; set; } = "";
    public DateTime Date { get; set; } = DateTime.Now;
}
