using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{
    public Slider slider;
    public ScoreDisplayer sd;

    void Start()
    {
        sd = GameObject.Find("Scores").GetComponent<ScoreDisplayer>();
    }

    void Update()
    {
        slider.value = sd.GetRatio();
    }
}
