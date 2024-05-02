//using LinearAlgebraLib;

//namespace GeometryTests
//{
//    using static GeometryLib.ConvexHull;

//    public class ConvexHullTests
//    {
//        readonly Vec3[] m_vertices;
//        readonly static Random _random = new Random(42);

//        public static void Shuffle<T>(T[] array)
//        {
//            int n = array.Length;
//            while (n > 1)
//            {
//                n--;
//                int k = _random.Next(n + 1);
//                T value = array[k];
//                array[k] = array[n];
//                array[n] = value;
//            }
//        }

//        public ConvexHullTests()
//        {
//            m_vertices =
//            [
//                new Vec3(28.903, 142.162, 0),
//                new Vec3(72.446, 229.837, 0),
//                new Vec3(130.01, 138.694, 0),
//                new Vec3(215.397, 137.735, 0),
//                new Vec3(217.795, 205.852, 0),
//                new Vec3(258.57, 274.45, 0),
//                new Vec3(172.224, 280.206, 0),
//            ];
//        }

//        [Fact]
//        public void MeshFindsNeigboursCorrectlyAllOrderedCorrectly_TwoFaces()
//        {
//            // Arrange
//            Mesh mesh = new Mesh(m_vertices);

//            // Act
//            mesh.ConnectAndAdd(0, 1, 2);
//            mesh.ConnectAndAdd(2, 1, 6);

//            // Assert
//            Assert.Equal(2, mesh.Faces.Count);

//            Face target;

//            target = mesh.Faces[0];
//            Assert.Null(target.Adjacent[0]);
//            Assert.Equal(mesh.Faces[1], target.Adjacent[1]);
//            Assert.Null(target.Adjacent[2]);

//            target = mesh.Faces[1];
//            Assert.Equal(mesh.Faces[0], target.Adjacent[0]);
//            Assert.Null(target.Adjacent[1]);
//            Assert.Null(target.Adjacent[2]);
//        }

//        [Fact]
//        public void MeshFindsNeigboursCorrectlyIncorrectOrder_TwoFaces()
//        {
//            // Arrange
//            Mesh mesh = new Mesh(m_vertices);

//            // Act
//            mesh.Add(1, 2, 0);
//            mesh.Add(1, 2, 6);

//            // Assert
//            Assert.Equal(2, mesh.Faces.Count);

//            Face target;

//            target = mesh.Faces[0];
//            Assert.Equal(mesh.Faces[1], target.Adjacent[0]);
//            Assert.Null(target.Adjacent[1]);
//            Assert.Null(target.Adjacent[2]);
//        }
//    }
//}
