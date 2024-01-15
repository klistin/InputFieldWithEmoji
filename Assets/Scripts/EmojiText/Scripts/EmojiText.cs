using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using System.Text;

public class EmojiText : Text
{
    //private const string regex1 = @"[0-9|#]\u20E3|(\uD83C[\uDD00-\uDDFF])+|\uD83C[\uDE00-\uDFFF][\uFE0F]?|\uD83D[\uDC00-\uDE4F][\uFE0F]?|[\u2600-\u27FF][\uFE0F]?|[\u2934-\u2935][\uFE0F]?|[\u2B00-\u2BFF][\uFE0F]?|\u3030[\uFE0F]?|\u303D[\uFE0F]?|\u3297[\uFE0F]?|\u3299[\uFE0F]?";
    //private const string regex1 = @"[0-9|#]\u20E3|[\uD800-\uDBFF][\uDC00-\uDFFF][\uFE0F]?|[\u2600-\u27FF][\uFE0F]?|[\u2934-\u2935][\uFE0F]?|[\u2B00-\u2BFF][\uFE0F]?|\u3030[\uFE0F]?|\u303D[\uFE0F]?|\u3297[\uFE0F]?|\u3299[\uFE0F]?";

    private static string Regex1 =
        @"[0-9|#]\u20E3" +
        @"|[\u0023|\u002a|\u0030-\u0039]\ufe0f\u20e3" + 
        @"|[\u261d|\u270a-\u270d]\ud83c[\udffb-\udfff]" +  
        @"|\u26f9\ufe0f\u200d[\u2640|\u2642]\ufe0f" + 
        @"|\u26f9\ud83c[\udffb-\udfff]\u200d[\u2640|\u2642]\ufe0f" + 
        @"|\ud83c[\udfc3|\udfcb]\ud83c[\udffb-\udfff]\u200d[\u2640|\u2642]\ufe0f" + 
        @"|\ud83c[\udfc3|\udfc4|\udfca]\u200d[\u2640|\u2642]\ufe0f"  + 
        @"|\ud83c[\udfcb-\udfcc]\ufe0f\u200d[\u2640|\u2642]\ufe0f" + 
        @"|\ud83c[\udfc4|\udfca|\udfcc]\ud83c[\udffb-\udfff]\u200d[\u2640|\u2642]\ufe0f" + 
        @"|\ud83c[\udf85|\udfc3|\udfc7]\ud83c[\udffb-\udfff]" + 
        @"|\ud83c\udff4\u200d\u2620\ufe0f" + 
        @"|\ud83c\udff3\ufe0f\u200d\ud83c\udf08" + 
        @"|\ud83c[\ud000-\udfff]" + 
        @"|\ud83d[\ude45|\ude46|\ude47|\ude4b|\ude4d|\ude4e|\udc6e|\udc6f|\udc71|\udc73|\udc77|\udc81|\udc82|\udc86|\udc87|\udea3-\udeb6]\u200d[\u2640|\u2642]\ufe0f" + 
        @"|\ud83d[\udc68-\udc69]\u200d\ud83e[\uddb0-\uddb3]" + 
        @"|\ud83d[\udc68-\udc69]\u200d[\u2695-\u2696]\ufe0f" + 
        @"|\ud83d[\udc68-\udc69]\u200d\ud83c[\udf3e|\udf73|\udf93|\udfa4|\udfa8|\udfeb|\udfed]" + 
        @"|\ud83d[\udc68-\udc69]\u200d\ud83d[\udc66-\udc69]\u200d\ud83d[\udc66-\udc67]\u200d\ud83d[\udc66-\udc67]" + 
        @"|\ud83d[\udc68-\udc69]\u200d\ud83d[\udc66-\udc69]\u200d\ud83d[\udc66-\udc67]" + 
        @"|\ud83d[\udc41|\udc68-\udc69]\u200d\ud83d[\udc66|\udc67|\udcbb|\udcbc|\udd27|\udd2c|\udde8|\ude80|\ude92]" + 
        @"|\ud83d[\udc68-\udc69]\u200d\u2708\ufe0f"  + 
        @"|\ud83d[\udc68-\udc69]\u200d\u2764\ufe0f\u200d\ud83d[\udc68-\udc69]" + 
        @"|\ud83d[\udc68-\udc69]\u200d\u2764\ufe0f\u200d\ud83d\udc8b\u200d\ud83d[\udc68-\udc69]"+ 
        @"|\ud83d[\udc68-\udc69]\ud83c\udffb\u200d[\u2695-\u2696]\ufe0f" + 
        @"|\ud83d[\udc68-\udc69]\ud83c\udffb\u200d\ud83c[\udf3e|\udf73|\udf93|\udfa4|\udfa8|\udfeb|\udfed]" + 
        @"|\ud83d[\udc68-\udc69]\ud83c\udffb\u200d\ud83d[\udcbb|\udcbc|\udd27|\udd2c|\ude80|\ude92]" + 
        @"|\ud83d[\udc68-\udc69]\ud83c[\udffb-\udfff]\u200d[\u2695-\u2696|\u2708][\ufe0f]?"  + 
        @"|\ud83d[\udc68-\udc69]\ud83c[\udffc-\udfff]\u200d\ud83c[\udf3e|\udf73|\udf93|\udfa8|\udfa4|\udfeb|\udfed]" + 
        @"|\ud83d[\udc68-\udc69]\ud83c[\udffc-\udfff]\u200d\ud83d[\udcbb|\udcbc|\udd27|\udd2c|\ude80|\ude92]" + 
        @"|\ud83d[\udc6e|\udea3-\udeb6]\ud83c[\udffb-\udfff]\u200d[\u2640|\u2642]\ufe0f"  + 
        @"|\ud83d[\udc71|\udc73|\udd75|\udc6e|\udc77|\udc82|\ude47]\ud83c[\udffb-\udfff]\u200d[\u2640|\u2642]\ufe0f" + 
        @"|\ud83d[\udc81|\ude45|\ude46|\ude4b|\ude4e|\ude4d|\udc86|\udc87]\ud83c[\udffb-\udfff]\u200d[\u2640|\u2642]\ufe0f" + 
        @"|\ud83d\udd75\ufe0f\u200d[\u2640|\u2642]\ufe0f" + 
        @"|\ud83d[\ud000-\udfff]\ud83c[\ud000-\udfff]"  + 
        @"|\ud83d[\ud000-\udfff]" + 
        @"|\ud83e[\uddb8-\uddb9]\u200d[\u2640|\u2642|\u2708]\ufe0f"  + 
        @"|\ud83e[\uddd9-\udddf]\u200d[\u2640|\u2642]\ufe0f"  + 
        @"|\ud83e[\udd26|\udd37-\udd39|\udd3c-\udd3d|\udd3e|\uddd6]\u200d[\u2640|\u2642]\ufe0f"  + 
        @"|\ud83e[\udd26|\udd37-\udd39|\udd3d|\udd3e|\uddd7|\uddd8]\ud83c[\udffb-\udfff]\u200d[\u2640|\u2642]\ufe0f"  + 
        @"|\ud83e[\ud000-\udfff]\ud83c[\ud000-\udfff]"  + 
        @"|\ud83e[\ud000-\udfff]"  + 
        @"|[\uD800-\uDBFF][\uDC00-\uDFFF][\uFE0F]?" +
        @"|\u00a9|\u00ae";
    private const string regex2 = @"<sprite=([a-z0-9A-Z]+)>";

    public static string ReplacementTxt = "%?";
    public static char ZWJ = '\u200d';//零宽度连接符
    public static char TEXT_PRESENTATION_SELECTOR = '\ufe0f';//文本表示选择符

    private Dictionary<int, EmojiFrame> emojiDic = new Dictionary<int, EmojiFrame>();

    private static EmojiAsset emojiData;
    private static readonly Dictionary<string, EmojiFrame> emojiFrames = new Dictionary<string, EmojiFrame>();
    private static readonly Dictionary<string, string> emojiFastSearch = new Dictionary<string, string>();

    #region Private Fields

    private float m_IconScaleOfDoubleSymbole = 1.0f;
    private bool m_EmojiParsingRequired = true;
    
    private PreRenderText preRenderText = new PreRenderText();

    #endregion

    public bool supportEmoji = true;

    private readonly UIVertex[] m_TempVerts = new UIVertex[4];

    public override float preferredWidth => cachedTextGeneratorForLayout.GetPreferredWidth(emojiText, GetGenerationSettings(rectTransform.rect.size)) / pixelsPerUnit;

    public override float preferredHeight => cachedTextGeneratorForLayout.GetPreferredHeight(emojiText, GetGenerationSettings(rectTransform.rect.size)) / pixelsPerUnit;

    public string emojiText => EmojiUtils.GetRenderStr(text);


    public override void SetVerticesDirty()
    {
        m_EmojiParsingRequired = supportEmoji;

        base.SetVerticesDirty();
    }
    
    //计算emojiDic 
    private void CalculateEmojiDic( bool removeSpace)
    {
        emojiDic.Clear();

        if (supportEmoji)
        {
            int nParcedCount = 0;
            int nOffset = 0;
            //根据正则表达式匹配出符合regex2 的值，然后根据匹配项算出emojiDic,emojiDic的key为实际绘制的索引值
            MatchCollection matches = Regex.Matches(preRenderText.GetPreRenderText(supportRichText,removeSpace),regex2);
            for (int i = 0; i < matches.Count; i++)
            {
                if (matches[i].Groups.Count > 1)
                {
                    if (emojiFrames.TryGetValue(matches[i].Groups[1].Value, out EmojiFrame info))
                    {
                        emojiDic.Add(matches[i].Index - nOffset + nParcedCount, info);
                    }
                    nOffset += matches[i].Length - 1;
                    nParcedCount++;
                }
            }
        }
    }

    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        if (font == null)
            return;

        // 初始化静态emoji库
        if (emojiData.frames == null || emojiData.frames.Length == 0)
        {
            TextAsset asset = Resources.Load<TextAsset>("texturepacker_EmojiData");
            if (asset != null)
            {
                emojiData = JsonUtility.FromJson<EmojiAsset>(asset.text);

                for (int i = 0; i < emojiData.frames.Length; ++i)
                {
                    string key = "";
                    string filename = emojiData.frames[i].filename;
                    filename = System.IO.Path.GetFileNameWithoutExtension(filename);
                    string[] codes = filename.Split('-');
                    for (int j = 0; j < codes.Length; ++j)
                    {
                        key += codes[j].PadLeft(8, '0');
                    }

                    emojiFrames.Add(key, emojiData.frames[i]);
                }
            }
            else
            {
                Debug.Log("OnPopulateMesh : asset is null!!!");
            }
        }

        if (m_EmojiParsingRequired)
        {
            ParseText();
        }

        // We don't care if we the font Texture changes while we are doing our Update.
        // The end result of cachedTextGenerator will be valid for this instance.
        // Otherwise we can get issues like Case 619238.
        m_DisableFontTextureRebuiltCallback = true;

        Vector2 extents = rectTransform.rect.size;

        var settings = GetGenerationSettings(extents);
        var renderText = emojiText;
        cachedTextGenerator.Populate(renderText, settings);

        // Apply the offset to the vertices
        IList<UIVertex> verts = cachedTextGenerator.verts;
        float unitsPerPixel = 1 / pixelsPerUnit;
        int vertCount = verts.Count;
        if (cachedTextGenerator.characterCount > 0)
        {
            if (renderText.Contains(" ") || renderText.Contains(Environment.NewLine))
            {
                var visibleCount = cachedTextGenerator.characterCountVisible;
                var showRenderText = renderText.Substring(0, visibleCount);
                //回车不占顶点数，所以把回车也干掉
                var removeSpaceText = showRenderText.Replace(" ", String.Empty).Replace(Environment.NewLine,String.Empty);
                if(supportRichText)
                    removeSpaceText = RichLabelUtils.RemoveLabels(removeSpaceText);
                //如果移除空格后发现和 vertCount/4 一样说明空格就没被计算，需要移除
                bool isNeedRemoveSpace = removeSpaceText.Length == vertCount/4;
                CalculateEmojiDic(isNeedRemoveSpace);
            }
            else
            {
                CalculateEmojiDic(false);
            }
        }

        toFill.Clear();
        bool needSetOffX = false;
        for (int i = 0; i < vertCount; ++i)
        {
            int index = i / 4;
            if (emojiDic.TryGetValue(index, out EmojiFrame info))
            {
                //compute the distance of '[' and get the distance of emoji 
                //计算%?的距离
                float emojiSize = 1.45f * (verts[i + 1].position.x - verts[i].position.x) * m_IconScaleOfDoubleSymbole;

                float fCharHeight = verts[i + 1].position.y - verts[i + 2].position.y;
                float fCharWidth = verts[i + 1].position.x - verts[i].position.x;

                float fHeightOffsetHalf = (emojiSize - fCharHeight) * 0.5f;
                float fStartOffset = 4 + emojiSize * (1 - m_IconScaleOfDoubleSymbole);
                
                if (i > 4)
                {
                    //判断% 与上一个？是不是在一行
                    bool isWrapLine = Math.Abs(verts[i].position.y - verts[i-4].position.y) > fCharHeight;
                    if (needSetOffX && !isWrapLine)
                    {
                        //如果%与上一个？在同一行 并且需要设置偏移
                        fStartOffset -= (verts[i].position.x - verts[i - 4].position.x );
                    }
                    else if(i+ 4 < vertCount)
                    {
                        //判断% 与 ？是否换行 换行就要设置偏移
                        needSetOffX = Math.Abs(verts[i].position.y - verts[i+4].position.y) > fCharHeight;
                    }
                }


                m_TempVerts[3] = verts[i];//1
                m_TempVerts[2] = verts[i + 1];//2
                m_TempVerts[1] = verts[i + 2];//3
                m_TempVerts[0] = verts[i + 3];//4
                
                m_TempVerts[0].position += new Vector3(fStartOffset, -fHeightOffsetHalf, 0);
                m_TempVerts[1].position += new Vector3(fStartOffset - fCharWidth + emojiSize, -fHeightOffsetHalf, 0);
                m_TempVerts[2].position += new Vector3(fStartOffset - fCharWidth + emojiSize, fHeightOffsetHalf, 0);
                m_TempVerts[3].position += new Vector3(fStartOffset, fHeightOffsetHalf, 0);

                m_TempVerts[0].position *= unitsPerPixel;
                m_TempVerts[1].position *= unitsPerPixel;
                m_TempVerts[2].position *= unitsPerPixel;
                m_TempVerts[3].position *= unitsPerPixel;

                float x = info.frame.x / 2048;
                float y = (2048 - info.frame.y - 32) / 2048;
                float size = info.sourceSize.w / 2048;


                float pixelOffset = size / 64;
                m_TempVerts[0].uv1 = new Vector2(x + pixelOffset, y + pixelOffset);
                m_TempVerts[1].uv1 = new Vector2(x - pixelOffset + size, y + pixelOffset);
                m_TempVerts[2].uv1 = new Vector2(x - pixelOffset + size, y - pixelOffset + size);
                m_TempVerts[3].uv1 = new Vector2(x + pixelOffset, y - pixelOffset + size);

                toFill.AddUIVertexQuad(m_TempVerts);

                i += 4 * 2 - 1;
            }
            else
            {
                needSetOffX = false;
                int tempVertsIndex = i & 3;
                m_TempVerts[tempVertsIndex] = verts[i];
                m_TempVerts[tempVertsIndex].position *= unitsPerPixel;
                if (tempVertsIndex == 3)
                    toFill.AddUIVertexQuad(m_TempVerts);
            }
        }

      
        m_DisableFontTextureRebuiltCallback = false;
    }
    
    
    string ReplaceFirst(string text, string search, string replace)
    {
        int pos = text.IndexOf(search, StringComparison.Ordinal);
        if (pos < 0)
        {
            return text;
        }
        return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
    }

    private void ProcessPreRenderTxt(string emoji)
    {
        if (emojiFastSearch.TryGetValue(emoji, out string key))
        {
            preRenderText.ReplaceFirst(emoji,string.Format("<sprite={0}>", key));
        }
        else
        {
            byte[] bytes = Encoding.UTF32.GetBytes(emoji);
            int length = bytes.Length;
            string highStr = "";
            string lowStr = "";
            if (length == 4)
            {
                for (int j = 0; j < 4; ++j)
                {
                    lowStr = bytes[j].ToString("x2") + lowStr;
                }
                key = highStr + lowStr;
            }
            else if (length == 8)
            {
                for (int j = 0; j < 4; ++j)
                {
                    highStr = bytes[j].ToString("x2") + highStr;
                }
                for (int j = 4; j < 8; ++j)
                {
                    lowStr = bytes[j].ToString("x2") + lowStr;
                }
                key = highStr + lowStr;
            }
            else if (length % 4 == 0)
            {
                string jointStr = "";
                string tempStr = "";
                for (int num = 0; num < length; ++num)
                {
                    jointStr = bytes[num].ToString("x2") + jointStr;
                    if (num%4 == 3)
                    {
                        tempStr = tempStr + jointStr;
                        jointStr = "";
                    }
                }

                key = tempStr;
            }

            bool find = emojiFrames.TryGetValue(key, out EmojiFrame frame);

            if (!find)
            {
                if (highStr == "" && lowStr == "")
                {
                    key = key + "0000fe0f";
                    find |= emojiFrames.TryGetValue(key, out frame);
                }
                else if (highStr == "" && lowStr != "")
                {
                    key = lowStr + "0000fe0f";
                    find |= emojiFrames.TryGetValue(key, out frame);
                }
                else if (lowStr == "0000fe0f")
                {
                    key = highStr;
                    find |= emojiFrames.TryGetValue(key, out frame);
                }
                else if (lowStr == "000020e3")
                {
                    key = highStr + "0000fe0f" + lowStr;
                    find |= emojiFrames.TryGetValue(key, out frame);
                }
            }

            if (find)
            {
                //Debug.LogFormat("{0} find by {1}", key, frame.filename);
                emojiFastSearch.Add(emoji, key);
                preRenderText.ReplaceFirst(emoji,string.Format("<sprite={0}>", key));
            }
            else
            {
                //Debug.LogFormat("{0} not find !!!", key);
                var defaultKey = "00002753";
                emojiFastSearch.Add(emoji, defaultKey);
                preRenderText.ReplaceFirst(emoji,string.Format("<sprite={0}>", defaultKey));
            }
        }
    }

    //将text中的emoji编码替换成key=sprite,value= utf32编码的键值对
    void ParseText()
    {
        if (supportEmoji)
        {
            preRenderText.SetRenderText(text);
            EmojiUtils.HandleTextWithCallbacks(text, (charArr) =>
            {
                ProcessPreRenderTxt(string.Concat(charArr));
            },null);
        }

        m_EmojiParsingRequired = false;

    }
}


[Serializable]
struct EmojiRect
{
    public float x, y, w, h;
}

[Serializable]
struct EmojiFloat2
{
    public float x, y;
}

[Serializable]
struct EmojiSize
{
    public float w, h;
}

[Serializable]
struct EmojiFrame
{
    public string filename;
    public EmojiRect frame;
    public bool rotated;
    public bool trimmed;
    public EmojiRect spriteSourceSize;
    public EmojiSize sourceSize;
    public EmojiFloat2 pivot;
}

[Serializable]
struct TexturePackerMeta
{
    public string app;
    public string version;
    public string image;
    public string format;
    public EmojiSize size;
    public float scale;
    public string smartupdate;
}

[Serializable]
struct EmojiAsset
{
    public EmojiFrame[] frames;
    public TexturePackerMeta meta;
}
