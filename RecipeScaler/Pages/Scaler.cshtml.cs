using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeScaler.Models;

namespace RecipeScaler.Pages
{
    // The PageModel for the Sacler Razor Page
    // Handles both GET and POST requests
    public class ScalerModel : PageModel
    {
        // Binds a list of ingredients submitted from the form to this property
        // Each ingredient contains a name, quantity, and unit
        [BindProperty]
        public List<RecipeIngredient> Ingredients { get; set; } = new();

        // Binds the original number of servings the recipe is designed for
        [BindProperty]
        public int OriginalServings { get; set; }

        // Binds the number of serving the user wants to scale the recipe to
        [BindProperty]
        public int DesiredServings { get; set; }

        // This list will hold the scaled ingredients after processing
        // It is populated in OnPost and displayed in the UI after form submission
        public List<RecipeIngredient> ScaledIngredients { get; set; } = new();

        // Handles POST requests (form submission)
        // Sacles the input ingredients based on the ratio of desired to original servings
        // This is pretty basic for now, just to get something down.
        // Will update to convert to apropriate units later.
        public void OnPost()
        {
            // Proceed only if both input values are valid and greater than zero
            if (OriginalServings > 0 && DesiredServings > 0)
            {
                // Calculate the scale factor (e.g., 2/4 = 0.5 for halving the recipe)
                double scaleFactor = (double)DesiredServings / OriginalServings;

                // Create a new list of scaled ingredients
                ScaledIngredients = Ingredients.Select(i => new RecipeIngredient
                {
                    Name = i.Name,
                    Quantity = Math.Round(i.Quantity * scaleFactor, 2), // Round to 2 decimal places for better readability
                    Unit = i.Unit
                }).ToList();
            }
        }

        // Handles GET requests (initial page load)
        // Ensures the page loads with one empty ingredient row so the form doesn't break
        public void OnGet()
        {
            Ingredients = new List<RecipeIngredient>
            {
                new RecipeIngredient() // Add a blank row for user input
            };
        }
    }
}
