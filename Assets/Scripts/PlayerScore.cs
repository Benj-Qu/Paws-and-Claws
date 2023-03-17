using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private float score = 0;
    private int flagNum = 0;

    public int coefficient = 3;

    private void Update()
    {
        score += flagNum * Time.deltaTime;
    }

    public void updateScore(float delta)
    {
        score += delta;
    }

    public int getScore()
    {
        return Mathf.FloorToInt(score * coefficient);
    }

    public void getFlag()
    {
        flagNum++;
    }

    public void loseFlag()
    {
        flagNum--;
    }

    public void resetFlag()
    {
        flagNum = 0;
    }

    public void reset()
    {
        flagNum = 0;
        score = 0;
    }
}
