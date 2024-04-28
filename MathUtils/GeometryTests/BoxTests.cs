using GeometryLib;
using LinearAlgebraLib;

namespace GeometryTests
{
    public class BoxTests
    {
        [Fact]
        public void GetPointsReturnsCorrectValue()
        {
            // Arrange
            Box box = new Box(-100, -200, -300, 100, 200, 300);
            Vec3[] expected =
            [
                new Vec3(-100, -200, -300),
                new Vec3(-100, -200, 300),
                new Vec3(-100, 200, -300),
                new Vec3(-100, 200, 300),

                new Vec3(100, -200, -300),
                new Vec3(100, -200, 300),
                new Vec3(100, 200, -300),
                new Vec3(100, 200, 300),
            ];

            // Act
            Vec3[] actual = box.GetPoints();

            // Assert
            Assert.NotNull(actual);
            Assert.Equal(actual?.Length, 8);

            for (int i = 0; i < 8; i++)
            {
                Assert.Equal(expected[i], actual![i]);
            }
        }
    }
}
