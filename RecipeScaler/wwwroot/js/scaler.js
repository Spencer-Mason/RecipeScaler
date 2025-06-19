// This file contains JavaScript for the Recipe Scaler application

// Tracks the index of the next ingredient row
// This ensures proper model binding for the ingredients
let ingredientIndex = 1;

// Function to add a new ingredient row dynamically
function addIngredient() {
    const container = document.getElementById("ingredients-container");

    // Create a new div representing one ingredient row
    const row = document.createElement("div");
    row.className = "row mb-2 ingredient-row";

    // Define the HTML structure for the new ingredient row, using current index
    row.innerHTML = `
        <div class="col">
            <input name="Ingredients[${ingredientIndex}].Name" class="form-control" placeholder="Ingredient name" />
        </div>
        <div class="col">
            <input name="Ingredients[${ingredientIndex}].Quantity" class="form-control" placeholder="Quantity" />
        </div>
        <div class="col">
            <input name="Ingredients[${ingredientIndex}].Unit" class="form-control" placeholder="Unit (e.g. cups)" />
        </div>`;

    // Append the row to the container and increment the index
    container.appendChild(row);
    ingredientIndex++;
}
