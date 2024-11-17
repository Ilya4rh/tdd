using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudVisualization.Visualization;

public class ImageSaver
{
    public static void Save(Bitmap bitmap, string filePath, string fileName, ImageFormat imageFormat)
    {
        bitmap.Save(Path.Combine(filePath, fileName), imageFormat);
    }
}