namespace Test;

public class UnitTest
{
    [Fact]
    public void TrueShouldBeFalse()
    {
        true.Should().BeFalse();
    }
    
    [Fact]
    public void TrueShouldBeTrue()
    {
        true.Should().BeTrue();
    }
}