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
            return new Rectangle(center, rectangleSize);

        return new Rectangle(new Point(center.X + 1, center.Y + 2), rectangleSize);
    }
}