using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class textController : MonoBehaviour
{
    public Text text;
    private Queue<string> scripts = new Queue<string>();

    public void Start()
    {
        //scripts.Enqueue("[speed=0.1]<b>Guadians are strong. They can climb high walls!</b>");
        //scripts.Enqueue("我从2015年开始在CSDN写博客，到现在有近[speed=0.2]<size=40>两万粉丝</size>啦。");
        //scripts.Enqueue("感谢大家的[speed=0.3]<size=40>关注</size>与<b>支持</b>。");
        //scripts.Enqueue("我会持续输出Unity[speed=0.3]干货文章[speed=0.1]，希望可以帮助到想学Unity的同学，共勉!");
        //scripts.Enqueue("好了，那么我们下一篇文章再见，[speed=0.4]<i>拜拜</i>。");

        //ShowScript();
    }

    private void ShowScript()
    {
        if (scripts.Count <= 0)
        {
            return;
        }

        text.TypeText(scripts.Dequeue(), onComplete: () => Debug.Log("TypeText Complete"));
    }

    public void updateText(string text)
    {
        scripts.Enqueue(text);
        ShowScript();
    }

    /// <summary>
    /// 对白被点击
    /// </summary>
    public void OnClickWindow()
    {
        if (text.IsSkippable())
        {
            // 跳过，直接显示全部文本
            text.SkipTypeText();
        }
        else
        {
            ShowScript();
        }
    }
}
