/// <summary>
/// Container for the shopping list stored in browser localStorage.
/// </summary>
public class ShoppingListData
{
    public List<ShoppingItem> ShoppingItems { get; set; } = new();
}

/// <summary>
/// Represents a single item on the shopping list.
/// Quantity reflects count for countable items; RecipeRequirement shows
/// the unit-based measurement needed (e.g., "2 Cups", "30 Count").
/// </summary>
public class ShoppingItem
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Quantity { get; set; }
    public bool IsChecked { get; set; }
    public string RecipeRequirement { get; set; } = "";
}
