using LinearAlgebraLib;

namespace LinearAlgebraTests
{
    public class Vec3Tests
    {
        [Fact]
        public void DeconstructWorksCorrectly()
        {
            // Arrange
            Vec3 v = new Vec3(12, 34, -2);

            // Act
            v.Deconstruct(out double actualX, out double actualY, out double actualZ);

            // Assert
            Assert.Equal(v.x, actualX);
            Assert.Equal(v.y, actualY);
            Assert.Equal(v.z, actualZ);
        }
    }
}
