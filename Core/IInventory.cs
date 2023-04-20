﻿namespace Core;

public interface IInventory
{
    IEnumerable<Ingredient> Ingredients { get; }
    void RegisterIngredient(Ingredient ingredient);
}