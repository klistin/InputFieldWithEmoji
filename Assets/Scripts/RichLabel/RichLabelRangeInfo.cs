
//富文本标签信息的结构

using System;

class LabelMatchInfo
{
    public int Index;
    public int Length;

    public int GetEndPos()
    {
        return Index + Length;
    }
}

//整个富文本标签的结构  分为前后两个部分  前: <color = #aaaaaa>  后: </color>
class RichLabelRangeInfo
{
    private RichLabelType richLabelType;
    public LabelMatchInfo FrontLabelInfo;
    public LabelMatchInfo BackLabelInfo;

    public RichLabelRangeInfo(RichLabelType richType)
    {
        richLabelType = richType;
    }

    //是否在富文本标记范围  如<color=#aa0000>aaa</color> 的 <color=#aa0000> 和 </color>
    public bool IsInTagRange(int index)
    {

        if (index >= FrontLabelInfo.Index && index < FrontLabelInfo.GetEndPos())
        {
            return true;
        }
    
        if (index >= BackLabelInfo.Index && index < BackLabelInfo.GetEndPos())
        {
            return true;
        }

        return false;
    }

    //是否是富文本的文本  如<color=#aa0000>aaa</color> 的 aaa
    public bool IsInRichLabelRange(int index)
    {
        if (index >= FrontLabelInfo.GetEndPos() && index < BackLabelInfo.Index)
        {
            return true;
        }

        return false;
    }

    public string GetBackLabel()
    {
        string result = String.Empty;
        switch (richLabelType)
        {
            case RichLabelType.Color:
                result = "</color>";
                break;
            case RichLabelType.Bold:
                result = "</b>";
                break;
            case RichLabelType.Size:
                result = "</size>";
                break;
            case RichLabelType.Italic:
                result = "</i>";
                break;
            default:
                break;
        }

        return result;
    }
}