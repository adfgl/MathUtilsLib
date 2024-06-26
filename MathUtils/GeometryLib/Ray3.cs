﻿using LinearAlgebraLib;

namespace GeometryLib
{
    public readonly struct Ray3
    {
        public readonly Vec3 origin;
        public readonly Vec3 direction;

        public Ray3(Vec3 origin, Vec3 direction)
        {
            this.origin = origin;
            this.direction = direction.Normalize();
        }

        public Vec3 PointAlong(double distance)
        {
            return origin + direction * distance;
        }
    }
}
