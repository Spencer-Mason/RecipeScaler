﻿// This file contains JavaScript for the Recipe Scaler application

// Tracks the index of the next ingredient row
// This ensures proper model binding for the ingredients
let ingredientIndex = document.querySelectorAll('.ingredient-row').length;

// Function to add a new ingredient row dynamically
function addIngredient() {
    ingredientIndex = document.querySelectorAll('.ingredient-row').length;
    const container = document.getElementById("ingredients-container");

    // Create a new div representing one ingredient row
    const row = document.createElement("div");
    row.classList.add('row', 'mb-2', 'ingredient-row');

    // Define the HTML structure for the new ingredient row, using current index
    row.innerHTML = `
        <div class="col">
            <input name="Ingredients[${ingredientIndex}].Name" class="form-control" placeholder="Ingredient name" />
        </div>
        <div class="col">
            <input name="Ingredients[${ingredientIndex}].Quantity" class="form-control" placeholder="Quantity" type="number" step="any" required />
        </div>
        <div class="col">
            <select name="Ingredients[${ingredientIndex}].Unit" class="form-control">
                <option value="cup">Cups</option>
                <option value="tbsp">Tablespoons(tbsp)</option>
                <option value="tsp">Teaspoons(tsp)</option>
                <option value="ml">Milliliters(ml)</option>
                <option value="l">Liters(l)</option>
                <option value="g">Grams(g)</option>
                <option value="kg">Kilograms(kg)</option>
                <option value="oz">Ounces(oz)</option>
                <option value="lb">Pounds(lb)</option>
                <option value="unit">Units(e.g. 1 egg)</option>
            </select>
        </div>
        <div class="col-auto">
            <button type="button" class="btn btn-danger" onclick="removeIngredient(this)">Remove</button>
        </div>`;

    // Append the row to the container and increment the index
    container.appendChild(row);
    ingredientIndex++;
    renumberIngredients();
    row.querySelector('input')?.focus();
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

    // Update the index after removal
    renumberIngredients();
}

/**
 * Renumbers the ingredient rows to ensure proper model binding.
 */
function renumberIngredients() {
    const rows = document.querySelectorAll('.ingredient-row');
    rows.forEach((row, index) => {
        const inputs = row.querySelectorAll('input, select');
        inputs.forEach(input => {
            const name = input.name.replace(/\[\d+\]/, `[${index}]`);
            input.name = name;
        });
    });
    ingredientIndex = rows.length; // Update the global index
}