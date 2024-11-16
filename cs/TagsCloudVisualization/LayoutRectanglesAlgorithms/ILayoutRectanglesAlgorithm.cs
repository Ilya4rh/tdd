using System.Drawing;

namespace TagsCloudVisualization.LayoutRectanglesAlgorithms;

public interface ILayoutRectanglesAlgorithm
{
    Rectangle PutNextRectangle(Size rectangleSize, Point cloudCenter);
}