namespace ModernApi.Tests.DbUp;

using Mocella.DbUp;
using Xunit;

public class Given_EnvParser
{
    [Fact]
    public void NullShouldBeUndefined()
    {
        AssertStringParsesAsExpected(null, Env.Undefined);
    }

    [Fact]
    public void EmptyStringShouldBeProd()
    {
        AssertStringParsesAsExpected(string.Empty, Env.Prod);
    }

    [Fact]
    public void PRODShouldBePROD()
    {
        AssertStringParsesAsExpected("PROD", Env.Prod);
    }

    [Fact]
    public void DEVShouldBeDEV()
    {
        AssertStringParsesAsExpected("DEV", Env.DEV);
    }

    [Fact]
    public void ShouldNotBeCaseSensitive()
    {
        AssertStringParsesAsExpected("dev", Env.DEV);
    }

    private void AssertStringParsesAsExpected(string? value, Env expected)
    {
        var actual = EnvParser.Parse(value);
        Assert.Equal(expected, actual);
    }
}