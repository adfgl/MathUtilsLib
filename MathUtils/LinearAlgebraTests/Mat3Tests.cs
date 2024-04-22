using LinearAlgebraLib;

namespace LinearAlgebraTests
{
    public class Mat3Tests
    {
        [Fact]
        public void GetThrowsIndexOutOfRangeException()
        {
            var m = new Mat3(1, 2, 3, 4, 5, 6, 7, 8, 9);
            Assert.Throws<IndexOutOfRangeException>(() => m.Get(3, 0));
            Assert.Throws<IndexOutOfRangeException>(() => m.Get(0, 3));
            Assert.Throws<IndexOutOfRangeException>(() => m.Get(-1, 0));
            Assert.Throws<IndexOutOfRangeException>(() => m.Get(0, -1));
        }

        [Fact]
        public void GetReturnsCorrectCellValue()
        {
            Mat3 m = new Mat3(1, 2, 3, 4, 5, 6, 7, 8, 9);
            Assert.Equal(m.m11, m.Get(0, 0));
            Assert.Equal(m.m12, m.Get(0, 1));
            Assert.Equal(m.m13, m.Get(0, 2));

            Assert.Equal(m.m21, m.Get(1, 0));
            Assert.Equal(m.m22, m.Get(1, 1));
            Assert.Equal(m.m23, m.Get(1, 2));

            Assert.Equal(m.m31, m.Get(2, 0));
            Assert.Equal(m.m32, m.Get(2, 1));
            Assert.Equal(m.m33, m.Get(2, 2));
        }
    }
}
