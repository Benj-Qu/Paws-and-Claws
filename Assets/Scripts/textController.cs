using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class textController : MonoBehaviour
{
    public Text text;
    private Queue<string> scripts = new Queue<string>();
    string level = "";

    public void Start()
    {
        level = SceneManager.GetActiveScene().name;
        if (level == "Story")
        {
            scripts.Enqueue("[speed=0.1]<b>Guadians are strong. They can climb high walls!</b>");
            ShowScript();
        }
    }

    private void ShowScript()
    {
        if (scripts.Count <= 0)
        {
            return;
        }
        text.TypeText(scripts.Dequeue(), onComplete: () => {
            Debug.Log("TypeText Complete");
            if (level == "Story" && scripts.Count == 0)
            {
                // SceneManager.LoadScene("NewIntro");
            }
        });
        
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
