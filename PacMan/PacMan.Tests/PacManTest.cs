using System.Collections.Generic;
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
        #region FindTheBestCoordinate
        [Fact]
        public void ShouldHaveTheRIghtCoordinateWhenFindTheBestCoordinate()
        {
            // Arrange
            var currentCoordinates = new Coordinates
            {
                Abscissa = 1,
                Ordinate = 3
            };
            var wishedCoordinates = new Coordinates
            {
                Abscissa = 2,
                Ordinate = 4
            };
            var possibleCoordinates = new List<Coordinates>
            {
                new Coordinates
                {
                    Abscissa = 2,
                    Ordinate = 5
                },
                new Coordinates
                {
                    Abscissa = 10,
                    Ordinate = 4
                },
                wishedCoordinates
            };

            // Act
            var actualCoordinate = Game.FindTheBestCoordinate(currentCoordinates, possibleCoordinates);

            // Assert
            Assert.Equal(wishedCoordinates, actualCoordinate);
        }

        [Fact]
        public void ShouldHaveNullWhenFindTheBestCoordinateWithAtLeastEmptyParameter()
        {
            // Arrange
            var currentCoordinates = new Coordinates
            {
                Abscissa = 1,
                Ordinate = 3
            };
            var possibleCoordinates = new List<Coordinates>();
            Coordinates wishedCoordinates = null;

            // Act
            var actualCoordinate = Game.FindTheBestCoordinate(currentCoordinates, possibleCoordinates);

            // Assert
            Assert.Equal(wishedCoordinates, actualCoordinate);
        }
        #endregion
    }
}
