using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeScaler.Models;

namespace RecipeScaler.Pages
{
    public class ScalerModel : PageModel
    {
        [BindProperty]
        public List<RecipeIngredient> Ingredients { get; set; } = new();

        [BindProperty]
        public int OriginalServings { get; set; }

        [BindProperty]
        public int DesiredServings { get; set; }

        public List<RecipeIngredient> ScaledIngredients { get; set; } = new();

        public void OnPost()
        {
            if (OriginalServings > 0 && DesiredServings > 0)
            {
                double scaleFactor = (double)DesiredServings / OriginalServings;

                ScaledIngredients = Ingredients.Select(i => new RecipeIngredient
                {
                    Name = i.Name,
                    Quantity = Math.Round(i.Quantity * scaleFactor, 2),
                    Unit = i.Unit
                }).ToList();
            }
        }

        public void OnGet()
        {
            Ingredients = new List<RecipeIngredient>
            {
                new RecipeIngredient()
            };
        }
    }
}
