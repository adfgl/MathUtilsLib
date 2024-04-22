using LinearAlgebraLib;

namespace LinearAlgebraTests
{
    public class Vec2Tests
    {
        [Fact]
        public void DeconstructorWorksCorrectly()
        {
            // Arrange
            Vec2 v = new Vec2(12, 34, -2);

            // Act
            v.Deconstruct(out double actualX, out double actualY);

            // Assert
            Assert.Equal(v.x, actualX);
            Assert.Equal(v.y, actualY);
        }
    }
}