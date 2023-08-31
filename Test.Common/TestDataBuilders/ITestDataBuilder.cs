namespace Test.Unit.TestDataBuilders;

internal interface ITestDataBuilder<out T>
{
    T Build();
}