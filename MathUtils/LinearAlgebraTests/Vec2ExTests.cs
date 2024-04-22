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
    }
}
