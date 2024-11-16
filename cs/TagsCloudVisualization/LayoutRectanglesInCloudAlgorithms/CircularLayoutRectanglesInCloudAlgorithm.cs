using System.Drawing;

namespace TagsCloudVisualization.LayoutRectanglesInCloudAlgorithms;

public class CircularLayoutRectanglesInCloudAlgorithm : ILayoutRectanglesInCloudAlgorithm
{
    private readonly List<Rectangle> addedRectangles = [];
    private double currentAngleOfCircle;
    private double currentRadiusOfCircle;
    private const double OneDegree = Math.PI / 180;
    private const double FullCircleRotation = 2 * Math.PI;
    
    public Rectangle PutNextRectangle(Size rectangleSize, Point cloudCenter)
    {
        Rectangle rectangle;

        do
        {
            var x = cloudCenter.X + (int)(currentRadiusOfCircle * Math.Cos(currentAngleOfCircle)) - rectangleSize.Width / 2;
            var y = cloudCenter.Y + (int)(currentRadiusOfCircle * Math.Sin(currentAngleOfCircle)) - rectangleSize.Height / 2;
            rectangle = new Rectangle(new Point(x, y), rectangleSize);

            // увеличиваем угол на 1 градус
            currentAngleOfCircle += OneDegree; 
            
            // проверяем не прошли ли целый круг
            if (currentAngleOfCircle > FullCircleRotation)
            {
                currentAngleOfCircle = 0;
                currentRadiusOfCircle++;
            }
        } while (IntersectWithAddedRectangles(rectangle));
        
        addedRectangles.Add(rectangle);
        
        return rectangle;
    }
    
    private bool IntersectWithAddedRectangles(Rectangle rectangle)
    {
        return addedRectangles.Any(addedRectangle => addedRectangle.IntersectsWith(rectangle));
    }
}