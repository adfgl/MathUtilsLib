using GeometryLib;

namespace GeometryLib
{
    public interface IObject
    {
        Box BoundingBox { get; }
    }

    public class OctreeTriangle3<T> where T : IObject
    {
        //public void Add(Box box, T item)
        //{

        //}

        //public List<T> Query(Box box)
        //{

        //}
    }
}
