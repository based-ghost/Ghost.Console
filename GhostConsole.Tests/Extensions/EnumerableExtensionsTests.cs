namespace GhostConsole.Tests.Extensions;

public class EnumerableExtensionsTests
{
    #region IsNullOrEmpty
    [Theory]
    [InlineData(null)]
    [InlineData(new int[0])]
    public void IsNullOrEmpty_TrueCases(IEnumerable<int> source)
    {
        var result = source.IsNullOrEmpty();
        Assert.True(result);
    }

    [Theory]
    [InlineData(new[] { 2 })]
    public void IsNullOrEmpty_FalseCases(IEnumerable<int> source)
    {
        var result = source.IsNullOrEmpty();
        Assert.False(result);
    }
    #endregion
}
