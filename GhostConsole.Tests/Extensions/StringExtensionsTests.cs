namespace GhostConsole.Tests.Extensions;

public class StringExtensionsTests
{
    #region ExtractDigits
    [Theory]
    [InlineData("Test Case 1", "1")]
    [InlineData("2nd Test Case", "2")]
    [InlineData("Th3 3rd Test Cas3", "333")]
    [InlineData("", "")]
    [InlineData(" ", "")]
    public void ExtractDigits_TrueCases(string source, string expected)
    {
        var actual = source.ExtractDigits();
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("Test Case 1", "1 ")]
    [InlineData("Second Test Case", "2")]
    [InlineData("Th3 third Test Case", "33")]
    [InlineData("", null)]
    public void ExtractDigits_FalseCases(string source, string expected)
    {
        var actual = source.ExtractDigits();
        Assert.NotEqual(expected, actual);
    }
    #endregion

    #region Truncate
    [Theory]
    [InlineData("Test Case 1", 3)]
    [InlineData("Test Case 2", 5)]
    [InlineData("", 0)]
    public void Truncate_TrueCases(string source, int maxChars)
    {
        var result = source.Truncate(maxChars);
        Assert.Equal(maxChars, result?.Length);
    }
    #endregion
}
