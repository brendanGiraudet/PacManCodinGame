using Xunit;

namespace PacMan.Tests
{
    public class PacManTest
    {
        [Fact]
        public void ShouldHaveGoodDistanceWhenGetDistance()
        {
            // Arrange
            var currentCoordinates = new Coordinates
            {
                Abscissa = 1,
                Ordinate = 3
            };
            var expectedCoordinates = new Coordinates
            {
                Abscissa = 2,
                Ordinate = 5
            };
            var expectedDistance = 3;

            // Act
            var actualDistance = currentCoordinates.GetDistance(expectedCoordinates);

            // Assert
            Assert.Equal(expectedDistance, actualDistance);

        }
    }
}
