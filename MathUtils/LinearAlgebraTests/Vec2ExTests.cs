using LinearAlgebraLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearAlgebraTests
{
    public class Vec2ExTests
    {
        [Fact]
        public void BetweenReturnsCorrectValue()
        {
            // Arrange
            Vec2 a = new Vec2(0, 0);
            Vec2 b = new Vec2(80, 80);
            Vec2 expected = new Vec2(40, 40);

            // Act
            Vec2 actual = a.Between(b);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 0, 1, 0, true)]
        [InlineData(1, 1, 0, 1, false)]
        public void ParallelReturnsCorrectValue(double ax, double ay, double bx, double by, bool expected)
        {
            // Arrange
            Vec2 a = new Vec2(ax, ay);
            Vec2 b = new Vec2(bx, by);

            // Act
            bool actual = a.Parallel(b, 0.0001);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 0, 0, 1, true)]
        [InlineData(1, 1, 0, 1, false)]
        public void PerpendicularReturnsCorrectValue(double ax, double ay, double bx, double by, bool expected)
        {
            // Arrange
            Vec2 a = new Vec2(ax, ay);
            Vec2 b = new Vec2(bx, by);

            // Act
            bool actual = a.Perpendicular(b, 0.0001);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DistanceReturnsCorrectValue()
        {
            // Arrange
            Vec2 a = new Vec2(0, 0);
            Vec2 b = new Vec2(3, 4);
            double expected = 5;

            // Act
            double actual = a.Distance(b);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
