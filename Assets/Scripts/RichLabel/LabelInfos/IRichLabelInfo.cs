
public interface IRichLabelInfo
{
    bool IsRichText(string str);
    string RemoveLabel(string str);

    string GetRegexStr();

    RichLabelType GetLabelType();
}