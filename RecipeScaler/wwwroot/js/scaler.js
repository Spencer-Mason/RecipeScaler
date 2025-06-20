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
        </div>
        <div class="col-auto">
            <button type="button" class="btn btn-danger" onclick="removeIngredient(this)">Remove</button>
        </div>`;

    // Append the row to the container and increment the index
    container.appendChild(row);
    ingredientIndex++;
}

/**
 * Removes the ingredient row containing the clicked "Remove" button.
 * @param {HTMLElement} button - The button that was clicked
 */
function removeIngredient(button) {
    const container = document.getElementById("ingredients-container");

    // Only allow removal if more than one ingredient row exists
    const rows = container.getElementsByClassName("ingredient-row");
    if (rows.length <= 1) {
        alert("You must have at least one ingredient.");
        return;
    }

    const row = button.closest(".ingredient-row");
    if (row) {
        row.remove();
    }
}