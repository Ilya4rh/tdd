using System.Drawing;

namespace TagsCloudVisualization;

public class CircularCloudLayouter : ICircularCloudLayouter
{
    private readonly Point center;
    private readonly List<Rectangle> addedRectangles;
    private double currentAngleOfSpiral;
    private double currentRadiusOfSpiral;
    
    public CircularCloudLayouter(Point center)
    {
        this.center = center;
        addedRectangles = new List<Rectangle>();
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        Rectangle rectangle;

        do
        {
            var x = center.X + (int)(currentRadiusOfSpiral * Math.Cos(currentAngleOfSpiral)) - rectangleSize.Width / 2;
            var y = center.Y + (int)(currentRadiusOfSpiral * Math.Sin(currentAngleOfSpiral)) - rectangleSize.Height / 2;
            rectangle = new Rectangle(new Point(x, y), rectangleSize);

            // увеличиваем угол на 1 градус
            currentAngleOfSpiral += Math.PI / 180; 
            
            // проверяем не прошли ли целый круг
            if (currentAngleOfSpiral > 2 * Math.PI)
            {
                currentAngleOfSpiral = 0;
                currentRadiusOfSpiral++;
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