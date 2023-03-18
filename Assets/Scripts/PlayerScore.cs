using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private float score = 0;
    private int flagNum = 0;

    public int coefficient = 3;

    private bool fierce = false;
    private int coef = 1;

    private void Update()
    {
        score += coef * flagNum * Time.deltaTime;
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

    public void Fierce()
    {
        if (!fierce)
        {
            fierce = true;
            StartCoroutine(FinalBattle(10f));
        }
    }

    private IEnumerator FinalBattle(float duration)
    {
        coef = 2;
        yield return new WaitForSeconds(duration);
        coef = 1;
        fierce = false;
    }
}
