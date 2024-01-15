using System.Text.RegularExpressions;

public class ItalicRichLabel : IRichLabelInfo
{
    private string regex = @"(<i>)((?!</i>).)*(</i>)";
    public bool IsRichText(string str)
    {
        return Regex.IsMatch(str, regex);
    }

    public string RemoveLabel(string str)
    { 
        return Regex.Replace(str, @"<i>|</i>", "");
    }

    public string GetRegexStr()
    {
        return regex;
    }
    
    public RichLabelType GetLabelType()
    {
        return RichLabelType.Italic;
    }
}