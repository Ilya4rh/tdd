using System.Drawing;

namespace TagsCloudVisualization.LayoutRectanglesInCloudAlgorithms;

public class CircularLayoutAlgorithm : ILayoutAlgorithm
{
    private readonly double stepIncreasingAngle;
    private readonly double stepIncreasingRadius;
    private double currentAngleOfCircle;
    private double currentRadiusOfCircle;
    private const double OneDegree = Math.PI / 180;
    private const double FullCircleRotation = 2 * Math.PI;

    public CircularLayoutAlgorithm(double stepIncreasingAngle = OneDegree, double stepIncreasingRadius = 1)
    {
        if (stepIncreasingAngle <= 0)
            throw new ArgumentException("The parameter 'stepIncreasingAngle' is less than or equal to zero");
        if (stepIncreasingRadius == 0)
            throw new ArgumentException("The parameter 'stepIncreasingRadius' is zero");
        
        this.stepIncreasingAngle = stepIncreasingAngle;
        this.stepIncreasingRadius = stepIncreasingRadius;
    }
    
    public Point CalculateNextPoint()
    {
        var x = (int)(currentRadiusOfCircle * Math.Cos(currentAngleOfCircle));
        var y = (int)(currentRadiusOfCircle * Math.Sin(currentAngleOfCircle));
        
        currentAngleOfCircle += stepIncreasingAngle; 
            
        // проверяем не прошли ли целый круг
        if (currentAngleOfCircle > FullCircleRotation)
        {
            currentAngleOfCircle = 0;
            currentRadiusOfCircle += stepIncreasingRadius;
        }

        return new Point(x, y);
    }
}