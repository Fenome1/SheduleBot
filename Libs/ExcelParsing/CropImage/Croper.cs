using Aspose.Imaging;

namespace ExcelParsing.CropImage;
public class Croper : IDisposable
{
    private readonly RasterImage _image;

    private const int LShift = 25;
    private const int RShift = 460;
    private const int TShift = 25;
    private const int BShift = 150;
    public Croper(FileInfo file)
    {
        _image = (RasterImage)Image.Load(file.FullName);
    }

    public void CropImage()
    {
        _image.Crop(LShift, RShift, TShift, BShift);
        _image.Save();
    }

    public void Dispose()
    {
        _image.Dispose();
        GC.SuppressFinalize(this);
    }
}