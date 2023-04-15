using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class textController : MonoBehaviour
{
    public Text text;
    public TextMeshProUGUI StoryHint;
    private Queue<string> scripts = new Queue<string>();
    string level = "";

    public void Start()
    {
        level = SceneManager.GetActiveScene().name;
        StoryHint.enabled = false;
        //if (level == "Story")
        //{
        //    scripts.Enqueue("[speed=0.1]<b>Guadians are strong. They can climb high walls!</b>");
        //    ShowScript();
        //}
    }

    private void ShowScript()
    {
        if (scripts.Count <= 0)
        {
            return;
        }
        text.TypeText(scripts.Dequeue(), onComplete: () => {
            Debug.Log("TypeText Complete");
            if(level == "Farm" && text.text != "")
            {
                GameController.instance.guardian_speaking = 1; // finish speaking
                StoryHint.enabled = true;
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
