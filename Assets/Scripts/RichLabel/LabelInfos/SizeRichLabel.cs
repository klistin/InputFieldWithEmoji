using System.Text.RegularExpressions;


public class SizeRichLabel : IRichLabelInfo
{
    private string regex = @"(<size=[ ]*[0-9]+>)((?!</size>).)*(</size>)";
    public bool IsRichText(string str)
    {
        return Regex.IsMatch(str, regex);
    }
    
    public string RemoveLabel(string str)
    { 
        return Regex.Replace(str, @"<size=[ ]*[0-9]+>|</size>", "");
    }
    
    public string GetRegexStr()
    {
        return regex;
    }
    
    public RichLabelType GetLabelType()
    {
        return RichLabelType.Size;
    }
}