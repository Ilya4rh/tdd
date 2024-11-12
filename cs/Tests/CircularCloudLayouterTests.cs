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
        var c = new CircularCloudLayouter(center);
        var rectangleSize = new Size(rectangleWidth, rectangleHeight);

        var addedRectangle = c.PutNextRectangle(rectangleSize);

        addedRectangle.Location.Should().BeEquivalentTo(center);
    }
}