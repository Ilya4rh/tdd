using System.Drawing;

namespace TagsCloudVisualization;

public class CircularCloudLayouter : ICircularCloudLayouter
{
    private readonly Point center;
    private readonly List<Rectangle> addedRectangles;
    
    public CircularCloudLayouter(Point center)
    {
        this.center = center;
        addedRectangles = new List<Rectangle>();
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        if (addedRectangles.Count == 0)
        {
            var rectangleStartPoint =
                new Point(center.X - rectangleSize.Width / 2, center.Y - rectangleSize.Height / 2);
            var firstRectangle = new Rectangle(rectangleStartPoint, rectangleSize);
            addedRectangles.Add(firstRectangle);
            return firstRectangle;
        }
        

        return new Rectangle(new Point(center.X + 1, center.Y + 2), rectangleSize);
    }
}