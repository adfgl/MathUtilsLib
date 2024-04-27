﻿using LinearAlgebraLib;

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

        [Theory]
        [InlineData(0, 20, 0, 100)] // fully inside
        [InlineData(0, 0, 100, 100)] // on border
        [InlineData(0, 0, 150, 150)] // intersects border
        public void ExpansionWorksCorrectly(double x, double y, double z, double expectedRadius)
        {
            // Arrange
            BoundingSphere sphere = new BoundingSphere(0, 0, 0, 100);
            
            // Act
            BoundingSphere expanded = sphere.Expand(x, y, z);
            
            // Assert
            Assert.Equal(expectedRadius, expanded.radius, 6);
        }

        [Theory]
        [InlineData(22, 33, 0, 20, 100)] // fully inside
        [InlineData(0, 0, 50, 50, 100)] // on border
        [InlineData(0, 0, 100, 25, 125)] // on border
        public void ExpansionUsingOtherWorksCorrectly(double cx, double cy, double cz, double radius, double expectedRadius)
        {
            // Arrange
            BoundingSphere sphere = new BoundingSphere(0, 0, 0, 100);
            BoundingSphere other = new BoundingSphere(cx, cy, cz, radius);
            
            // Act
            BoundingSphere expanded = sphere.Expand(other);
            
            // Assert
            Assert.Equal(expectedRadius, expanded.radius, 6);
        }
    }
}
