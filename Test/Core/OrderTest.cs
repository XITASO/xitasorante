using Core;

namespace Test.Core;

public class OrderTest
{
    [Fact]
    public void I_can_add_a_dish()
    {
        var order = new Order();
        var dish = new Dish("Pizza Diavola", 9.5m, "Spicy, hot and delicios");
        order.AddDish(dish);

        order.Positions.Should().HaveCount(1);
        order.Positions.Single().Amount.Should().Be(1);
        order.Positions.Single().Dish.Should().Be(dish);
    }
    
    [Fact]
    public void I_can_add_two_dishes_of_the_same_kind()
    {
        var order = new Order();
        var dish1 = new Dish("Pizza Diavola", 9.5m, "Spicy, hot and delicios");
        var dish2 = new Dish("Pizza Diavola", 9.5m, "Spicy, hot and delicios");
        order.AddDish(dish1);
        order.AddDish(dish2);

        order.Positions.Should().HaveCount(1);
        order.Positions.Single().Amount.Should().Be(2);
        order.Positions.Single().Dish.Should().Be(dish1);
    }
    
    [Fact]
    public void I_can_add_two_of_a_different_kind()
    {
        var order = new Order();
        var dish1 = new Dish("Pizza Diavola", 9.5m, "Spicy, hot and delicios");
        var dish2 = new Dish("Cachopo", 14m, "Beef with filled with ham and cheese");
        order.AddDish(dish1);
        order.AddDish(dish2);

        var positions = order.Positions.ToList();

        positions.Should().HaveCount(2);
        positions.Should().BeEquivalentTo(new[]
        {
            new OrderPosition(dish1, 1),
            new OrderPosition(dish2, 1),
        });
    }
}