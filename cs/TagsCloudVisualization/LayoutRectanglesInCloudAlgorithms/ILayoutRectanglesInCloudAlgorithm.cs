using System.Drawing;

namespace TagsCloudVisualization.LayoutRectanglesInCloudAlgorithms;

public interface ILayoutRectanglesInCloudAlgorithm
{
    Rectangle PutNextRectangle(Size rectangleSize, Point cloudCenter);
}