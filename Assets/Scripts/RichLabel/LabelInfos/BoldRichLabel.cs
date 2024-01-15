using System.Text.RegularExpressions;

public class BoldRichLabel : IRichLabelInfo
{
    private string regex = @"(<b>)((?!</b>).)*(</b>)";
    public bool IsRichText(string str)
    {
        return Regex.IsMatch(str, regex);
    }
    
    public string RemoveLabel(string str)
    { 
        return Regex.Replace(str, @"<b>|</b>", "");
    }
    
    public string GetRegexStr()
    {
        return regex;
    }

    public RichLabelType GetLabelType()
    {
        return RichLabelType.Bold;
    }
}