using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace Tests;

[TestFixture]
public class CircularCloudLayouterTests
{
    [TestCase(0, 0, 10, 15)]
    [TestCase(2, 7, 5, 9)]
    public void PutNextRectangle_ShouldRectangleInCenter_WhenAddFirstRectangle(int centerX, int centerY, 
        int rectangleWidth, int rectangleHeight)
    {
        var center = new Point(centerX, centerY);
        var cloudLayouter = new CircularCloudLayouter(center);
        var rectangleSize = new Size(rectangleWidth, rectangleHeight);

        var addedRectangle = cloudLayouter.PutNextRectangle(rectangleSize);
        var expectedRectangleStartPoint = new Point(centerX - rectangleWidth / 2, centerY - rectangleHeight / 2);

        addedRectangle.Location.Should().BeEquivalentTo(expectedRectangleStartPoint);
    }
    
    [TestCase(10, 5, 15)]
    [TestCase(50, 30, 100)]
    [TestCase(100, 5, 50)]
    public void PutNextRectangle_ShouldAddedRectanglesDoNotIntersect(int countRectangles, int minSideLength,
        int maxSideLength)
    {
        var cloudLayouter = new CircularCloudLayouter(new Point(0, 0));
        var rectangleSizes = GetGeneratedRectangleSizes(countRectangles, minSideLength, maxSideLength);
        var addedRectangles = new List<Rectangle>();
        
        addedRectangles.AddRange(rectangleSizes.Select(t => cloudLayouter.PutNextRectangle(t)));

        for (var i = 0; i < addedRectangles.Count-1; i++)
        {
            addedRectangles
                .Skip(i + 1)
                .Any(addedRectangle => addedRectangle.IntersectsWith(addedRectangles[i]))
                .Should()
                .BeFalse();
        }
    }

    private static List<Size> GetGeneratedRectangleSizes(int countRectangles, int minSideLength, int maxSideLength)
    {
        var generatedSizes = new List<Size>();
        var random = new Random();
        
        for (var i = 0; i < countRectangles; i++)
        {
            var rectangleSize = new Size(random.Next(minSideLength, maxSideLength),
                random.Next(minSideLength, maxSideLength));
            
            generatedSizes.Add(rectangleSize);
        }

        return generatedSizes;
    }
}