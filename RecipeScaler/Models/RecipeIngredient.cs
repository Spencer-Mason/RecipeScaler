// Represents a single ingredient in a recipe
// This model is used for input, output, and scaling logic

namespace RecipeScaler.Models
{
    public class RecipeIngredient
    {
        // The name of the ingredient (e.g., "Flour", "Sugar")
        public string Name { get; set; } = "";
        // The quantity of the ingredient (e.g., 2.5 for 2.5 cups)
        public double Quantity { get; set; }
        // The unit of measurement for the ingredient (e.g., "cups", "grams")
        public string Unit { get; set; } = "";
    }
}
