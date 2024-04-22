using LinearAlgebraLib;

namespace LinearAlgebraTests
{
    public class MathHelperTests
    {
        [Theory]
        [InlineData(0, 0.1, true)]
        [InlineData(0.05, 0.1, true)]
        [InlineData(-0.05, 0.1, true)]
        [InlineData(0.2, 0.1, false)]
        [InlineData(-0.2, 0.1, false)]
        public void IsZeroReturnsCorrectResults(double number, double tolerance, bool expected)
        {
            bool result = MathHelper.IsZero(number, tolerance);
            Assert.Equal(expected, result);
        }
    }
}
