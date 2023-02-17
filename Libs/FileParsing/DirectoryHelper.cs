namespace FileParsing
{
    public static class DirectoryHelper
    {
        public static void InicializeDirectories()
        {
            Directory.CreateDirectory(Data.FolderPath.Shedule);
            Directory.CreateDirectory(Data.FolderPath.Excel);
            Directory.CreateDirectory(Data.FolderPath.Photo);
        }
    }
}
