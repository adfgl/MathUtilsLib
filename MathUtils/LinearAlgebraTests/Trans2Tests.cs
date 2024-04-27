using LinearAlgebraLib;

namespace LinearAlgebraTests
{
    public class Trans2Tests
    {
        [Fact]
        public void SingleTranslationWorksCorrectly()
        {
            // Arrange
            Vec2 v = new Vec2(20, 30);
            Vec2 expected = new Vec2(-30, 50);

            // Act
            Trans2 t = new Trans2().Translate(-50, 20);
            Vec2 actual = t.Forward(v);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ChainedTranslationWorksCorrectly()
        {
            // Arrange
            Vec2 v = new Vec2(20, 30);
            Vec2 expected = new Vec2(65, -150);

            // Act
            Trans2 t = new Trans2()
                .Translate(-50, 20)
                .Translate(100, -200)
                .Translate(-5, 0);
            Vec2 actual = t.Forward(v);

            // Assert
            Assert.Equal(expected, actual);
        }

    }
}
