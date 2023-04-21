namespace Test.TestDataBuilders;

internal interface ITestDataBuilder<out T>
{
    T Build();
}