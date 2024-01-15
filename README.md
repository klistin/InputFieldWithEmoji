# InputFieldWithEmoji


正常情况下Unity的InputField是不支持Emoji表情的，这是因为以下2点原因

1、InputField处理时候如果发现是emoji的就过滤了

2、Text字体渲染不支持，Unity 中Text就是用FreeType库来加载TrueType字体来实现的，将字体渲染到一张纹理上然后统一处理。而Cocos2dx 中的文本能显示是因为渲染的是系统渲染出来的纹理。




那如果要让InputField 能支持Emoji应该怎么办呢？

1、了解InputField和Text的实现原理。

2、了解Emoji编码原理。

3、了解了InputField实现的原理后，那么我们知道它是是怎么处理string的，以及emoji是怎么被过滤掉的，那我们就将这部分功能支持就好了，比如string 的处理改成StringInfo类处理。

4、了解了Text的实现原理后，我们修改shader，准备emoji资源，最后将emoji渲染出来。
