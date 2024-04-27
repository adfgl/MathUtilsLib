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

        [Fact]
        public void RotateAtWorksCorrectly()
        {
            // Arrange
            Vec2 v = new Vec2(30, 45);
            Vec2 expected = new Vec2(-35, 80);

            // Act
            Trans2 t = new Trans2().RotateAt(-20, 30, Math.PI / 2);
            Vec2 actual = t.Forward(v);

            // Assert
            Assert.True(t.IsDirty);
            for (int i = 0; i < 2; i++)
            {
                Assert.Equal(expected[i], actual[i], 3);
            }
        }

        [Fact]
        public void ScaleAtWorksCorrectly()
        {
            // Arrange
            Vec2 v = new Vec2(-35, 80);
            Vec2 expected = new Vec2(-57.5, 245);

            // Act
            Trans2 t = new Trans2().ScaleAt(-20, -30, 2.5, 2.5);
            Vec2 actual = t.Forward(v);

            // Assert
            Assert.True(t.IsDirty);
            for (int i = 0; i < 2; i++)
            {
                Assert.Equal(expected[i], actual[i], 3);
            }
        }

        [Fact]
        public void BackwardWorksCorrectly()
        {
            // Arrange
            Vec2 v = new Vec2(135.721, 131.821);
            Vec2 expected = new Vec2(50, 80);

            // Act
            Trans2 t = new Trans2()
                .Translate(50, 80)
                .RotateAt(-25, 30, Math.PI / 4)
                .Scale(2, 2)
                .Rotate(-Math.PI / 3)
                .ScaleAt(50, 77, 0.3, 0.3);
            Vec2 actual = t.Backward(v);

            // Assert
            Assert.True(t.IsDirty);
            for (int i = 0; i < 2; i++)
            {
                Assert.Equal(expected[i], actual[i], 3);
            }
        }

        [Fact]
        public void ResetsCorrectly()
        {
            // Arrange
            Vec2 v = new Vec2(55, 55);
            Trans2 t = new Trans2()
                .Translate(50, 80)
                .RotateAt(-25, 30, Math.PI / 4)
                .Scale(2, 2)
                .Rotate(-Math.PI / 3)
                .ScaleAt(50, 77, 0.3, 0.3);

            // Act
            t.Reset();
            Vec2 actual = t.Forward(v);

            // Assert
            Assert.False(t.IsDirty);
            Assert.Equal(v, actual);
        }
    }
}
