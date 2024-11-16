using System.Drawing;
using TagsCloudVisualization.LayoutRectanglesInCloudAlgorithms;

namespace TagsCloudVisualization;

public class CircularCloudLayouter : ICircularCloudLayouter
{
    private readonly Point center;
    private readonly ILayoutRectanglesInCloudAlgorithm layoutRectanglesInCloudAlgorithm;
    
    public CircularCloudLayouter(Point center, ILayoutRectanglesInCloudAlgorithm layoutRectanglesInCloudAlgorithm)
    {
        this.center = center;
        this.layoutRectanglesInCloudAlgorithm = layoutRectanglesInCloudAlgorithm;
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        var rectangle = layoutRectanglesInCloudAlgorithm.PutNextRectangle(rectangleSize, center);
        
        return rectangle;
    }
}