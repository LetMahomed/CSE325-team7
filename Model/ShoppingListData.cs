public class ShoppingListData
{
    public List<ShoppingItem> ShoppingItems { get; set; } = new();
}

public class ShoppingItem
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Quantity { get; set; }
    public bool IsChecked { get; set; }
}