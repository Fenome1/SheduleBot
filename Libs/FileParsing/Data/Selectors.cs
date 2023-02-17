namespace FileParsing.Data;
public record Selectors
{
    public static string Future =>
        "body > div.vt_hide > div.vt0.vt1.vt1a > div.vt4 > table > tbody > tr:nth-child(1) > td > span > strong > a";

    public static string Now =>
        "body > div.vt_hide > div.vt0.vt1.vt1a > div.vt4 > table > tbody > tr:nth-child(2) > td > span > strong > a";
}