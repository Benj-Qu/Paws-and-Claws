using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroFlagController : MonoBehaviour
{
    public string IconName;

    private GameObject icon;

    private void OnEnable()
    {
        icon = GameObject.Find(IconName);
    }

    private void Update()
    {
        if (icon)
        {
            if (icon.activeSelf)
            {
                float alpha = icon.GetComponent<SpriteRenderer>().color.a;
                Color color = Color.white;
                color.a = alpha / 2 + 0.5f;
                GetComponent<SpriteRenderer>().color = color;
            }
            else
            {
                Color color = Color.white;
                color.a = 0.5f;
                GetComponent<SpriteRenderer>().color = color;
            }
        }
    }
}
