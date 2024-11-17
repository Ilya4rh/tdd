using System.Drawing;
using TagsCloudVisualization.LayoutRectanglesInCloudAlgorithms;

namespace TagsCloudVisualization;

public class CircularCloudLayouter : ICircularCloudLayouter
{
    private readonly Point center;
    private readonly ILayoutAlgorithm layoutAlgorithm;
    private readonly List<Rectangle> addedRectangles = [];
    
    public CircularCloudLayouter(Point center, ILayoutAlgorithm layoutAlgorithm)
    {
        this.center = center;
        this.layoutAlgorithm = layoutAlgorithm;
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        Rectangle rectangle;

        do
        {
            var nextPoint = layoutAlgorithm.CalculateNextPoint();
            
            var x = center.X + nextPoint.X - rectangleSize.Width / 2;
            var y = center.Y + nextPoint.Y - rectangleSize.Height / 2;
            
            rectangle = new Rectangle(new Point(x, y), rectangleSize);

        } while (IntersectWithAddedRectangles(rectangle));
        
        addedRectangles.Add(rectangle);

        return rectangle;
    }
    
    private bool IntersectWithAddedRectangles(Rectangle rectangle)
    {
        return addedRectangles.Any(addedRectangle => addedRectangle.IntersectsWith(rectangle));
    }
}