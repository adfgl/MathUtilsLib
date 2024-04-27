using LinearAlgebraLib;

namespace LinearAlgebraTests
{
    public class BoundingSphereTests
    {
        [Theory]
        [InlineData(100, 100, 100, false)]
        [InlineData(0, 0, 100, true)]
        [InlineData(0, 50, 0, true)]
        public void ContainsWorksCorrectly(double x, double y, double z, bool expected)
        {
            // Arrange
            BoundingSphere sphere = new BoundingSphere(0, 0, 0, 100);
            
            // Act
            bool actual = sphere.Contains(x, y, z);
            
            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
