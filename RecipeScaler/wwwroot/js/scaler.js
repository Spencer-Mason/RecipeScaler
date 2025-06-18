let ingredientIndex = 1;

function addIngredient() {
    const container = document.getElementById("ingredients-container");
    const row = document.createElement("div");
    row.className = "row mb-2 ingredient-row";
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
    container.appendChild(row);
    ingredientIndex++;
}
