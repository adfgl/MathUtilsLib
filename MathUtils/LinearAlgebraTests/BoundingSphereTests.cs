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

        [Theory]
        [InlineData(25, 25, 25, 30, true)] // fully inside
        [InlineData(0, 0, 100, 30, false)] // intersects border
        [InlineData(0, 0, 75, 25, true)] // on border
        public void ContainsOtherWorksCorrectly(double cx, double cy, double cz, double radius, bool expected)
        {
            // Arrange
            BoundingSphere sphere = new BoundingSphere(0, 0, 0, 100);
            BoundingSphere other = new BoundingSphere(cx, cy, cz, radius);
            
            // Act
            bool actual = sphere.Contains(other);
            
            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
