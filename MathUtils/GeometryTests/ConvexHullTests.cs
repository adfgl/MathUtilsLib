//using GeometryLib;
//using LinearAlgebraLib;
//using System;
//using static GeometryLib.ConvexHull;

//namespace GeometryTests
//{
//    public class ConvexHullTests
//    {
//        public static List<Vec3> GenerateRandomPoints(int count, double size)
//        {
//            Random random = new Random();

//            List<Vec3> points = new List<Vec3>();

//            double minX = -size;
//            double maxX = size;
//            double minY = -size;
//            double maxY = size;
//            double minZ = -size;
//            double maxZ = size;

//            for (int i = 0; i < count; i++)
//            {
//                double randomX = random.NextDouble() * (maxX - minX) + minX;
//                double randomY = random.NextDouble() * (maxY - minY) + minY;
//                double randomZ = random.NextDouble() * (maxZ - minZ) + minZ;

//                points.Add(new Vec3(randomX, randomY, randomZ));
//            }

//            return points;
//        }

//        [Fact]
//        public void GetValidFace_ThrowsErrorWhenCentroidLiesOnFace()
//        {
//            // Arrange
//            Vec3[] points =
//            [
//                new Vec3(0, 100, 0),
//                new Vec3(-100, -100, 0),
//                new Vec3(+100, -100, 0),
//            ];

//            // Act
//            Action act = () => ConvexHull.GetValidFace(points, Vec3.Zero, 0, 1, 2, 0);

//            // Assert
//            Assert.Throws<InvalidOperationException>(act);
//        }

//        [Fact]
//        public void GetValidFace_FaceIsInvertedCorrectlyWhenReferencePointIsInFrontOfFace()
//        {
//            // Arrange
//            Vec3[] points =
//            [
//                new Vec3(0, 100, 0),
//                new Vec3(-100, -100, 0),
//                new Vec3(+100, -100, 0),
//            ];
//            Vec3 centroid = new Vec3(0, 0, 100);
//            Face expected = new Face(2, 1, 0, new Plane(new Vec3(0, 0, -1), 0));

//            // Act
//            Face actual = ConvexHull.GetValidFace(points, centroid, 0, 1, 2, 0);

//            // Assert
//            Assert.Equal(expected, actual);
//        }

//        [Fact]
//        public void GetValidFace_FaceIsNotInvertedWhenReferencePointIsBehindFace()
//        {
//            // Arrange
//            Vec3[] points =
//            [
//                new Vec3(+100, -100, 0),
//                new Vec3(-100, -100, 0),
//                new Vec3(0, 100, 0),
//            ];
//            Vec3 centroid = new Vec3(0, 0, 100);
//            Face expected = new Face(0, 1, 2, new Plane(new Vec3(0, 0, -1), 0));

//            // Act
//            Face actual = ConvexHull.GetValidFace(points, centroid, 0, 1, 2, 0);

//            // Assert
//            Assert.Equal(expected, actual);
//        }

//        [Fact]
//        public void InitialTetrahedronIsBuiltCorrectly()
//        {
//            // Arrange
//            Vec3 a = new Vec3(-100, -100, -50);
//            Vec3 b = new Vec3(0, +100, -50);
//            Vec3 c = new Vec3(+100, -100, -50);
//            Vec3 d = new Vec3(0, 0, 100);

//            Plane plane = new Plane(a, d, c);

//            Vec3[] points = [a, b, c, d];
//            Face abc = new Face(0, 1, 2, new Plane());
//            Face adc = new Face(0, 3, 2, new Plane());
//            Face bda = new Face(1, 3, 0, new Plane());
//            Face cdb = new Face(1, 3, 2, new Plane());

//            // Act
//            Tuple<Face[], int[]> actual = ConvexHull.InitialTetrahedron(points, 0);

//            // Assert
//            Assert.Equal(4, actual.Item1.Length);
//            Assert.Equal(4, actual.Item2.Length);

//            static void assertFace(Face expected, Face[] faces)
//            {
//                int index = -1;
//                for (int i = 0; i < 4; i++)
//                {
//                    Face face = faces[i];
//                    if (face.a == expected.a && face.b == expected.b && face.c == expected.c)
//                    {
//                        index = i;
//                        break;
//                    }
//                }
//                Assert.True(index != -1);
//            }

//            assertFace(abc, actual.Item1);
//            assertFace(adc, actual.Item1);
//            //assertFace(abd, actual.Item1);
//            //assertFace(bcd, actual.Item1);
//        }
//    }
//}
