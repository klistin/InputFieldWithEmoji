using System.Text.RegularExpressions;

public class ColorRichLabel : IRichLabelInfo
{
    //颜色标签
    private string regex = @"(<color[ ]*=[ ]*#[a-z0-9A-Z]+>)((?!</color>).)*(</color>)";
    //private string regex = @"(<color[ ]*=[ ]*(#)?[a-z0-9A-Z]+>)((?!</color>).)*(</color>)";
    public bool IsRichText(string str)
    {
        return Regex.IsMatch(str, regex);
    }
    
    public string RemoveLabel(string str)
    { 
        return Regex.Replace(str, @"<color[ ]*=[ ]*#[a-z0-9A-Z]+>|</color>", "");
        //return Regex.Replace(str, @"<color[ ]*=[ ]*(#)?[a-z0-9A-Z]+>|</color>", "");
    }
    
    public string GetRegexStr()
    {
        return regex;
    }

    public RichLabelType GetLabelType()
    {
        return RichLabelType.Color;
    }
}