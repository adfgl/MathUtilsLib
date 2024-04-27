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
            Assert.True(t.IsDirty);
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
            Assert.True(t.IsDirty);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SingleRotationWorksCorrectly()
        {
            // Arrange
            Vec2 v = new Vec2(20, 30);
            Vec2 expected = new Vec2(-30, 20);

            // Act
            Trans2 t = new Trans2().Rotate(Math.PI / 2);
            Vec2 actual = t.Forward(v);

            // Assert
            Assert.True(t.IsDirty);
            for (int i = 0; i < 2; i++)
            {
                Assert.Equal(expected[i], actual[i], 3);
            }
        }

        [Fact]
        public void ChainedRotationWorksCorrectly()
        {
            // Arrange
            Vec2 v = new Vec2(-30, 20);
            Vec2 expected = new Vec2(-11.554, -34.154);

            // Act
            Trans2 t = new Trans2()
                .Rotate(Math.PI / 2)
                .Rotate(-Math.PI / 4)
                .Rotate(Math.PI / 3);
            Vec2 actual = t.Forward(v);

            // Assert
            Assert.True(t.IsDirty);
            for (int i = 0; i < 2; i++)
            {
                Assert.Equal(expected[i], actual[i], 3);
            }
        }

        [Fact]
        public void SingleScalingWorksCorrectly()
        {
            // Arrange
            Vec2 v = new Vec2(20, 30);
            Vec2 expected = new Vec2(40, 60);

            // Act
            Trans2 t = new Trans2().Scale(2, 2);
            Vec2 actual = t.Forward(v);

            // Assert
            Assert.True(t.IsDirty);
            for (int i = 0; i < 2; i++)
            {
                Assert.Equal(expected[i], actual[i], 3);
            }
        }

        [Fact]
        public void ChainedScalingWorksCorrectly()
        {
            // Arrange
            Vec2 v = new Vec2(20, 30);
            Vec2 expected = new Vec2(30, 45);

            // Act
            Trans2 t = new Trans2()
                .Scale(2, 2)
                .Scale(0.25, 0.25)
                .Scale(3, 3);
            Vec2 actual = t.Forward(v);

            // Assert
            Assert.True(t.IsDirty);
            for (int i = 0; i < 2; i++)
            {
                Assert.Equal(expected[i], actual[i], 3);
            }
        }
    }
}
