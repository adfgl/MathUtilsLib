﻿using LinearAlgebraLib;

namespace LinearAlgebraTests
{
    public class MathHelperTests
    {
        [Theory]
        [InlineData(0, 0.1, true)]
        [InlineData(0.05, 0.1, true)]
        [InlineData(-0.05, 0.1, true)]
        [InlineData(0.2, 0.1, false)]
        [InlineData(-0.2, 0.1, false)]
        public void IsZeroReturnsCorrectResults(double number, double tolerance, bool expected)
        {
            bool result = MathHelper.IsZero(number, tolerance);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, 0.1, true)]
        [InlineData(1.05, 0.1, true)]
        [InlineData(0.95, 0.1, true)]
        [InlineData(1.2, 0.1, false)]
        [InlineData(0.8, 0.1, false)]
        public void IsOneReturnsCorrectResults(double number, double tolerance, bool expected)
        {
            bool result = MathHelper.IsOne(number, tolerance);
            Assert.Equal(expected, result);
        }
    }
}
