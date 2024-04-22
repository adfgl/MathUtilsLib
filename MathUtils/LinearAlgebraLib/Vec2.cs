﻿using System.Diagnostics;
using System.Dynamic;

namespace LinearAlgebraLib
{
    [DebuggerDisplay("{x} {y}")]
    public readonly struct Vec2
    {
        public readonly double x, y, w;

        public Vec2(double x, double y, double w = 1)
        {
            this.x = x;
            this.y = y;
            this.w = w;
        }

        public void Deconstruct(out double x, out double y)
        {
            x = this.x;
            y = this.y;
        }

        public double GetValue(int index)
        {
            switch (index)
            {
                case 0: return x;
                case 1: return y;
                case 2: return w;

                default:
                    throw new ArgumentOutOfRangeException(nameof(index));
            }
        }

        public double this[int index] => GetValue(index);
    }
}
