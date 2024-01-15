using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine.Assertions;

public class EmojiUtils
{
    //下个字符是不是零宽度连接符
    public static bool NextCharIsZWJ(string str,TextElementEnumerator elementEnumerator)
    {
        var nextStr = StringInfo.GetNextTextElement(str,
            elementEnumerator.ElementIndex + elementEnumerator.Current.ToString().Length);
        var charArr = nextStr.ToCharArray();
        if (nextStr.ToCharArray().Length != 1)
            return false;
        return charArr[0] == EmojiText.ZWJ;
    }
    
    //下个字符是不是选择符
    public static bool NextCharIsSelectorChar(string str,TextElementEnumerator elementEnumerator)
    {
        var nextStr = StringInfo.GetNextTextElement(str,
            elementEnumerator.ElementIndex + elementEnumerator.Current.ToString().Length);
        var charArr = nextStr.ToCharArray();
        if (nextStr.ToCharArray().Length != 1)
            return false;
        return charArr[0] == EmojiText.TEXT_PRESENTATION_SELECTOR;
    }
    
    //下个字符是不是控制符
    public static bool NextCharIsControl(string str,TextElementEnumerator elementEnumerator)
    {
        var nextStr = StringInfo.GetNextTextElement(str,
            elementEnumerator.ElementIndex + elementEnumerator.Current.ToString().Length);
        var charArr = nextStr.ToCharArray();
        if (charArr.Length == 2)
        {
            //modifier修饰符
            if (charArr[0] == '\ud83c')
            {
                return (CharUnicodeInfo.GetUnicodeCategory(charArr[1])  == UnicodeCategory.ModifierSymbol) 
                       || (charArr[1] >= '\udffb' && charArr[1] <= '\udfff');//皮肤颜色
                // return (charArr[1] >= '\udffb' && charArr[1] <= '\udfff'); //皮肤颜色
            }
            else if (charArr[0] == '\udb40')
            {
                //\udb40 通常是辅助平面字符的高位，它与低位代理对结合起来，形成一个完整的辅助平面字符
                return charArr[1] >= '\udc62' && charArr[1] <= '\udc7f';
            }
        }
        else if (charArr.Length == 1)
        {
            return charArr[0] == EmojiText.ZWJ || charArr[0] == EmojiText.TEXT_PRESENTATION_SELECTOR;
        }

        return false;
    }
    
    //下个字符是不是地域指示符
    public static bool NextCharIsCountryIndicator(string str,TextElementEnumerator elementEnumerator)
    {
        var nextStr = StringInfo.GetNextTextElement(str,
            elementEnumerator.ElementIndex + elementEnumerator.Current.ToString().Length);
        var charArr = nextStr.ToCharArray();
        if (charArr.Length == 2)
        {
            //modifier修饰符
            if (charArr[0] == '\ud83c')
            {
                return (charArr[1] >= '\udde6' && charArr[1] <= '\uddff') ;//国旗
            }
        }
     
        return false;
    }
    
    //判断当前字符是不是地域指示符
    public static bool CurCharIsCountryIndicator(char[] charArr)
    {
        if (charArr.Length == 2)
        {
            //modifier修饰符
            if (charArr[0] == '\ud83c')
            {
                return (charArr[1] >= '\udde6' && charArr[1] <= '\uddff') ;//国旗
            }
        }
     
        return false;
    }
    //判断当前字符是不是宽度连接符
    public static bool CharIsZWJ(char curChar)
    {
        return curChar == EmojiText.ZWJ ;
    }
    
    //判断当前字符是不是emoji
    public static bool CharIsEmoji(char curChar)
    {
        return (curChar >= '\u23e9' && curChar <= '\u23ff') || (curChar >= '\u2600' && curChar <= '\u26ff')  
               || (curChar >= '\u2702' && curChar <= '\u27bf') ;
    }
    
    public static bool CharIsSelector(char curChar)
    {
        return curChar == EmojiText.TEXT_PRESENTATION_SELECTOR ;
    }

    //获取渲染的字符串(即包括%？的字符串)
    public static string GetRenderStr(string text)
    {
        //这里为什么要用两个字符来代替呢，是因为一个如果不这么做留给emoji显示的地方就比较窄，显示不下
        var tempStr = new StringBuilder();
        HandleTextWithCallbacks(text, () =>
        {
            tempStr.Append(EmojiText.ReplacementTxt);
        }, (nonEmojiStr) =>
        {
            tempStr.Append(nonEmojiStr);
        }, () => false);
        return tempStr.ToString();
    }

    /// <summary>
    /// 处理带emoji的文本
    /// </summary>
    /// <param name="text">待处理的文本</param>
    /// <param name="emojiCallback">检测到emoji时回调</param>
    /// <param name="nonEmojiCallback">检测到普通文本回调</param>
    /// <param name="breakConditionCallback">条件断点回调</param>
    /// <param name="enumertorCallback">迭代回调回调</param>
    public static void HandleTextWithCallbacks(string text,Action emojiCallback,Action<string> nonEmojiCallback,Func<bool> breakConditionCallback,Action enumertorCallback = null)
    {
        bool isHasEmoji = false;
        var strEnumerator = StringInfo.GetTextElementEnumerator(text);
        while (strEnumerator.MoveNext())
        {
            enumertorCallback?.Invoke();
            var charArr = strEnumerator.Current.ToString().ToCharArray();
            if (charArr.Length == 2 && NextCharIsControl(text, strEnumerator))
            {
                isHasEmoji = true;
                continue;
            }

            if (!isHasEmoji && CurCharIsCountryIndicator(charArr) && NextCharIsCountryIndicator(text,strEnumerator))
            {
                isHasEmoji = true;
                continue;
            }

            if (charArr.Length > 1)
            {
                isHasEmoji = false;
                emojiCallback?.Invoke();
            }
            else
            {
                if (CharIsZWJ(charArr[0]) || NextCharIsZWJ(text,strEnumerator))
                    continue;

                if (isHasEmoji || EmojiUtils.CharIsEmoji(charArr[0]))
                {
                    emojiCallback?.Invoke();
                    isHasEmoji = false;
                }
                else
                {
                    nonEmojiCallback?.Invoke(strEnumerator.Current.ToString());
                }
            }
            if (breakConditionCallback != null && breakConditionCallback())
                break;
        }
    }

    /// <summary>
    /// 处理带emoji的文本
    /// </summary>
    /// <param name="text">待处理的文本</param>
    /// <param name="emojiTextCallback">回调返回emoji文本</param>
    /// <param name="normalTextCallback">回调返回普通文本</param>
    public static void HandleTextWithCallbacks(string text,Action<char []> emojiTextCallback,Action<char []> normalTextCallback){
        List<char> tempCharList = null;
            
        var strEnumerator = StringInfo.GetTextElementEnumerator(text);
        while (strEnumerator.MoveNext())
        {
            var charArr = strEnumerator.Current.ToString().ToCharArray();
            //如果遇到控制符就先临时存起来
            if (charArr.Length == 2 && NextCharIsControl(text, strEnumerator))
            {
                if (tempCharList == null)
                {
                    tempCharList = new List<char>(charArr);
                    continue;
                }
             
                tempCharList.AddRange(charArr);
                continue;
            }

            if ((tempCharList == null || tempCharList.Count == 0) && CurCharIsCountryIndicator(charArr) 
                                                                  && NextCharIsCountryIndicator(text, strEnumerator))
            {
                if (tempCharList == null)
                {
                    tempCharList = new List<char>(charArr);
                    continue;
                }
                tempCharList.AddRange(charArr);
                continue;
            }

            if (charArr.Length > 1)
            {
                if (tempCharList != null && tempCharList.Count > 0)
                {
                    tempCharList.AddRange(charArr);
                    emojiTextCallback(tempCharList.ToArray());
                    tempCharList.Clear();
                }
                else
                {
                    emojiTextCallback(charArr);
                }
            }
            else
            {
                var c = charArr[0];
                if (EmojiUtils.CharIsZWJ(c))
                {
                    Assert.IsNotNull(tempCharList, "Emojitext tempCharList == null");
                    tempCharList.Add(c);
                    continue;
                }
                if (tempCharList != null && tempCharList.Count > 0)
                {
                    tempCharList.AddRange(charArr);
                    emojiTextCallback(tempCharList.ToArray());
                    tempCharList.Clear();
                }
                else if (EmojiUtils.CharIsEmoji(c))
                {
                    emojiTextCallback(charArr);
                }
                else
                {
                    normalTextCallback?.Invoke(charArr);
                }
            }
            
        }
    }
}