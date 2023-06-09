﻿using Core;

namespace Infrastructure.Persistence;

public class InMemoryRecipeStore : IRecipeProvider, IMenu
{
    private readonly Dictionary<Dish, Recipe> recipes = new();

    public void Add(Recipe newRecipe)
    {
        recipes.Add(newRecipe.Dish, newRecipe);
    }

    public Recipe Get(Dish toFind)
    {
        return recipes[toFind];
    }

    public IList<Dish> GetMenu()
    {
        return recipes.Keys.ToList();
    }
}