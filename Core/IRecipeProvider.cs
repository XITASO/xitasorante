namespace Core;

public interface IRecipeProvider
{
    Recipe Get(Dish toFind);
    void Add(Recipe recipe);
}