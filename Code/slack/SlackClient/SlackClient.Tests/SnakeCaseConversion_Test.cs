using NUnit.Framework;
using SlackClient.Models;


namespace SlackClient.Tests
{
    [TestFixture]
    public class SnakeCaseConversion_Test
    {
        [TestCase("Snake", ExpectedResult = "snake")]
        [TestCase("Image36", ExpectedResult = "image_36")]
        [TestCase("snake case", ExpectedResult = "snake_case")]
        [TestCase("snakeCaseConversion", ExpectedResult="snake_case_conversion")]
        [TestCase("SnakeCaseConversion", ExpectedResult = "snake_case_conversion")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(null, ExpectedResult = null)]
        public string Can_Convert(string source) => SnakeCaseUtils.ToSnakeCase(source);
    }
}