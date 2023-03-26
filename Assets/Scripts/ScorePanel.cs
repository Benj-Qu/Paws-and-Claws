using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{
    public Slider slider;
    public ScoreDisplayer sd;

    private void Start()
    {
        sd = GameObject.Find("Scores").GetComponent<ScoreDisplayer>();
    }

    private void Update()
    {
        slider.value = sd.GetRatio();
    }

    public void reset()
    {
        sd.reset();
    }

    public int GetWinner()
    {
        return sd.GetWinner();
    }
}
