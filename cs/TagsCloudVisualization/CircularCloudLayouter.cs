using System.Drawing;
using TagsCloudVisualization.LayoutRectanglesAlgorithms;

namespace TagsCloudVisualization;

public class CircularCloudLayouter : ICircularCloudLayouter
{
    private readonly Point center;
    private readonly ILayoutRectanglesAlgorithm layoutRectanglesAlgorithm;
    
    public CircularCloudLayouter(Point center, ILayoutRectanglesAlgorithm layoutRectanglesAlgorithm)
    {
        this.center = center;
        this.layoutRectanglesAlgorithm = layoutRectanglesAlgorithm;
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        var rectangle = layoutRectanglesAlgorithm.PutNextRectangle(rectangleSize, center);
        
        return rectangle;
    }
}