using LinearAlgebraLib;

namespace LinearAlgebraTests
{
    public class BoundingBoxTests
    {
        [Fact]
        public void ConstructorThrowsOnInvalidBounds()
        {
            // Arrange
            Action act = () => new BoundingBox2(100, 200, -100, -200);

            // Act and assert
            ArgumentException ex = Assert.Throws<ArgumentException>(act);
        }

        [Theory]
        [InlineData(0, 0, true)] // actually inside
        [InlineData(100, 200, true)] // on border
        [InlineData(-200, 400, true)] // on node 
        [InlineData(-500, 1000, false)] // outside
        public void ContainsReturnsCorrectValue(double x, double y, bool expected)
        {
            // Arrange
            BoundingBox2 box = new BoundingBox2(-200, -300, 100, 400);

            // Act
            bool actual = box.Contains(x, y);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-117.412, 16.776, 26.539, 177.82, true)] // actually inside
        [InlineData(-43.951, 16.776, 100, 177.82, true)] // touching border
        [InlineData(23.125, 39.028, 167.076, 200.072, false)] // intersecting
        [InlineData(162.491, 49.568, 306.443, 210.612, false)] // outside
        [InlineData(-200, -300, 100, 400, true)] // identical/overlapping
        public void ContainsBoundingBoxReturnsCorrectValue(double minX, double minY, double maxX, double maxY, bool expected)
        {
            // Arrange
            BoundingBox2 box = new BoundingBox2(-200, -300, 100, 400);
            BoundingBox2 other = new BoundingBox2(minX, minY, maxX, maxY);

            // Act
            bool actual = box.Contains(other);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 0, -200, -300, 100, 400)] // does not expand
        [InlineData(-300, 500, -300, -300, 100, 500)] // expand top left
        [InlineData(213.583, 558.505, -200, -300, 213.583, 558.505)] // expand top right
        [InlineData(295.929, -450.438, -200, -450.438, 295.929, 400)] // expand bottom right
        [InlineData(-496.424, -508.995, -496.424, -508.995, 100, 400)] // expand bottom left
        public void ExpandReturnsCorrectValue(double x, double y, double expectedMinX, double expectedMinY, double expectedMaxX, double expectedMaxY)
        {
            // Arrange
            BoundingBox2 box = new BoundingBox2(-200, -300, 100, 400);

            // Act
            BoundingBox2 expanded = box.Expand(x, y);

            // Assert
            Assert.Equal(expectedMinX, expanded.minX, 3);
            Assert.Equal(expectedMinY, expanded.minY, 3);
            Assert.Equal(expectedMaxX, expanded.maxX, 3);
            Assert.Equal(expectedMaxY, expanded.maxY, 3);
        }
    }
}
