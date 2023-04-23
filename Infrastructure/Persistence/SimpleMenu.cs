using Core;

namespace Infrastructure.Persistence;

public static class SimpleMenu 
{
    public static IList<Dish> Menu = new List<Dish>
    {
        new ("Pizza Margeritha", 10.42M, "with cheese"),
        new ("Pizza Diavolo", 12.99M, "with hot salami & onions"),
        new ("Salad Casanova", 8.95M, "mixed salad with ham, cheese and eggs"),
        new ("Carbonara", 9.89M, "home made pasta with eggs, hard cheese and cured pork"),
        new ("Napoli", 8.42M, "with tomato sauce"),
        new ("Tiramisu", 6.30M, "home made"),
    };
}