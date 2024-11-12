using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization;
#pragma warning disable CA1416

public class VisualizationCircularCloudLayout
{
    public static void DrawAndSaveLayout(List<Rectangle> rectangles, string fileName, string filePath)
    {
        const int padding = 10;
        var minX = rectangles.Min(rectangle => rectangle.Left);
        var minY = rectangles.Min(rectangle => rectangle.Top);
        var maxX = rectangles.Max(rectangle => rectangle.Right);
        var maxY = rectangles.Max(rectangle => rectangle.Bottom);
        var width = maxX - minX + padding * 2;
        var height = maxY - minY + padding * 2;

        var random = new Random();
        
        using var bitmap = new Bitmap(width, height);
        using var graphics = Graphics.FromImage(bitmap);
        
        graphics.Clear(Color.White);
        
        foreach (var rectangle in rectangles)
        {
            var shiftedRectangle = rectangle with { X = rectangle.X - minX + padding, Y = rectangle.Y - minY + padding };
            
            var randomColor = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            
            using var brush = new SolidBrush(randomColor);
            graphics.FillRectangle(brush, shiftedRectangle);
            graphics.DrawRectangle(Pens.Black, shiftedRectangle);
        }
        
        bitmap.Save(Path.Combine(filePath, fileName), ImageFormat.Png);
    }
}