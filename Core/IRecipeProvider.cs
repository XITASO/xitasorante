namespace Core;

public interface IRecipeProvider
{
    Recipe Get(Dish toFind);
}