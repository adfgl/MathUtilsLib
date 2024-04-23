﻿using LinearAlgebraLib;

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
    }
}
