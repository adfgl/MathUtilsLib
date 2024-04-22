﻿namespace LinearAlgebraLib
{
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
    }
}
