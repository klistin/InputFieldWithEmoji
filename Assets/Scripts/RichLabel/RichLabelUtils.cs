using System;
using System.Collections.Generic;


public class RichLabelUtils
{
    private static readonly List<IRichLabelInfo> RichLabelInfos = new List<IRichLabelInfo>
    {
        new ColorRichLabel(),new BoldRichLabel(),new SizeRichLabel(),new ItalicRichLabel()
    };


    public static string RemoveLabels(string str)
    {
        foreach (var labelInfo in RichLabelInfos)
        {
            if (labelInfo.IsRichText(str))
            {
                str = labelInfo.RemoveLabel(str);
            }
        }

        return str;
    }

    public static void ForEach(Action<IRichLabelInfo> callBack)
    {
        foreach (var labelInfo in RichLabelInfos)
        {
            callBack(labelInfo);
        }
    }
}