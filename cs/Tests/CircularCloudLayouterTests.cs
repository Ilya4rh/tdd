using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization;
using TagsCloudVisualization.LayoutRectanglesAlgorithms;

namespace Tests;

[TestFixture]
public class CircularCloudLayouterTests
{
    private ICircularCloudLayouter cloudLayouter;
    private List<Rectangle> addedRectangles;
    
    [SetUp]
    public void Setup()
    {
        var circularLayoutAlgorithm = new CircularLayoutRectanglesAlgorithm();
        cloudLayouter = new CircularCloudLayouter(new Point(0, 0), circularLayoutAlgorithm);
        addedRectangles = [];
    }

    [TearDown]
    public void TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed) 
            return;
        
        var pathImageStored = TestContext.CurrentContext.TestDirectory + @"\imageFailedTests";

        if (!Directory.Exists(pathImageStored))
        {
            Directory.CreateDirectory(pathImageStored);
        }

        var testName = TestContext.CurrentContext.Test.Name;
        
        VisualizationCircularCloudLayout.DrawAndSaveLayout(addedRectangles, $"{testName}.png",
            pathImageStored);
            
        Console.WriteLine($@"Tag cloud visualization saved to file {pathImageStored}\{testName}.png");
    }
    
    [Test]
    public void PutNextRectangle_ShouldRectangleInCenter_WhenAddFirstRectangle()
    {
        var rectangleSize = new Size(10, 15);

        var addedRectangle = cloudLayouter.PutNextRectangle(rectangleSize);
        var expectedRectangleStartPoint = new Point(-addedRectangle.Width / 2, - addedRectangle.Height / 2);

        addedRectangle.Location.Should().BeEquivalentTo(expectedRectangleStartPoint);
    }
    
    [TestCase(10, 5, 15)]
    [TestCase(50, 30, 100)]
    [TestCase(100, 5, 50)]
    public void PutNextRectangle_ShouldAddedRectanglesDoNotIntersect(int countRectangles, int minSideLength,
        int maxSideLength)
    {
        var rectangleSizes = GetGeneratedRectangleSizes(countRectangles, minSideLength, maxSideLength);
        
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
    
    [TestCase(10, 5, 15)]
    [TestCase(50, 30, 100)]
    [TestCase(100, 5, 50)]
    public void CircleShape_ShouldBeCloseToCircle_WhenAddMultipleRectangles(int countRectangles, int minSideLength, 
        int maxSideLength)
    {
        var rectangleSizes = GetGeneratedRectangleSizes(countRectangles, minSideLength, maxSideLength);
        
        addedRectangles.AddRange(rectangleSizes.Select(t => cloudLayouter.PutNextRectangle(t)));

        var distances = addedRectangles
            .Select(rectangle => GetDistanceBetweenCenterRectangleAndCenterCloud(rectangle, new Point(0, 0)))
            .ToArray();

        for (var i = 1; i < distances.Length; i++)
        {
            var distanceBetweenRectangles = GetDistanceBetweenRectangles(addedRectangles[i], addedRectangles[i - 1]);
            distances[i].Should().BeApproximately(distances[i - 1], distanceBetweenRectangles);
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

    private static double GetDistanceBetweenCenterRectangleAndCenterCloud(Rectangle rectangle, Point center)
    {
        var centerRectangle = new Point(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2);
        
        return GetDistanceBetweenPoints(centerRectangle, center);
    }

    private static double GetDistanceBetweenRectangles(Rectangle rectangle1, Rectangle rectangle2)
    {
        var centerRectangle1 = new Point(rectangle1.X + rectangle1.Width / 2, rectangle1.Y + rectangle1.Height / 2);
        var centerRectangle2 = new Point(rectangle2.X + rectangle2.Width / 2, rectangle2.Y + rectangle2.Height / 2);

        return GetDistanceBetweenPoints(centerRectangle1, centerRectangle2);
    }

    private static double GetDistanceBetweenPoints(Point point1, Point point2)
    {
        return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
    }
}