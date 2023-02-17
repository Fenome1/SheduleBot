namespace FileParsing.Data;
public record FolderPath
{
    private static string CurrentDirectory => Environment.CurrentDirectory;
    public static string Shedule => CurrentDirectory + "/Shedule";
    public static string Excel => Shedule + "/Excel";
    public static string Photo => Shedule + "/Photo";
}