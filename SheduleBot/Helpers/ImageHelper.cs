using ExcelParsing.CropImage;

namespace SheduleBot.Helpers
{
    internal static class ImageHelper
    {
        public static void CropImage(FileInfo pngFile)
        {
            try
            {
                using var croper = new Croper(pngFile);
                croper.CropImage();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void CropImages(IEnumerable<FileInfo> pngFiles)
        {
            if (!pngFiles.Any()) { return; }

            foreach (var pngFile in pngFiles)
            {
               CropImage(pngFile);
            }
        }
    }
}
