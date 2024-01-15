using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/**
 * 目的：用于检测富文本标签范围，比如文本超出长度需要进行裁剪显示
 */
public class RichLabelRanges
{
    //富文本标识信息
    private List<RichLabelRangeInfo> richLabelRangeInfos = new List<RichLabelRangeInfo>();

    public int Count => richLabelRangeInfos.Count;
    
    //获取副文本标记的后缀部分
    public string GetBackLabel(int index)
    {
        for (int i = 0; i < richLabelRangeInfos.Count; i++)
        {
            if (richLabelRangeInfos[i].IsInRichLabelRange(index))
            {
                return richLabelRangeInfos[i].GetBackLabel();
            }
        }
        
        return String.Empty;
    }

    //是否在富文本标记范围 如<color=#aa0000>aaa</color> 的 <color=#aa0000> 和 </color>
    public bool IsInTagRange(int index)
    {
        for (int i = 0; i < richLabelRangeInfos.Count; i++)
        {
            if (richLabelRangeInfos[i].IsInTagRange(index))
            {
                return true;
            }
        }
        return false;
    }
    
    //初始化富文本标签信息
    public void InitRangeInfos(string text)
    {
        richLabelRangeInfos.Clear();
        RichLabelUtils.ForEach((labelInfo) =>
        {
            var match = Regex.Match(text, labelInfo.GetRegexStr());
            while (match.Success)
            {
                RichLabelRangeInfo richLabelRangeInfo = new RichLabelRangeInfo(labelInfo.GetLabelType());
                for (int i = 1; i < match.Groups.Count; i++) 
                {
                    Group g = match.Groups[i];
                    if (i == 1)
                    {
                        LabelMatchInfo labelMatchInfo = new LabelMatchInfo {Index = g.Index, Length = g.Length};
                        richLabelRangeInfo.FrontLabelInfo = labelMatchInfo;
                    }
                    else if(i == 3)
                    {
                        LabelMatchInfo labelMatchInfo = new LabelMatchInfo {Index = g.Index, Length = g.Length};
                        richLabelRangeInfo.BackLabelInfo = labelMatchInfo;
                    }
                }
                richLabelRangeInfos.Add(richLabelRangeInfo);
                match = match.NextMatch();
            }
        });
        
    }
    
}