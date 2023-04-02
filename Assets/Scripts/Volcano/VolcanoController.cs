using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoController : MonoBehaviour
{
    public float CeilHeight = 6f;

    public int RoundNum = 3;
    public float Delay = 1f;
    public float Interval = 10f;
    public float FireballDist = 0.05f;

    public float InitPeriod = 5f;

    public int FireballRound = 15;
    public int FireballNum = 3;
    public float LeftBorder = -7f;
    public float RightBorder = 7f;

    public GameObject Fireball;
    public AudioClip Burst;
    public AudioSource vas;

    public float volume = 1f;

    public void begin()
    {
        StartCoroutine(VolcanoCoroutine());
    }

    private IEnumerator VolcanoCoroutine()
    {
        yield return new WaitForSeconds(InitPeriod);
        for (int i = 0; i < RoundNum; i++)
        {
            vas.PlayOneShot(Burst, volume);
            yield return new WaitForSeconds(Delay);
            for (int j = 0; j < FireballRound; j++)
            {
                int num = Random.Range(1, FireballNum);
                for (int k = 0; k < FireballNum; k++)
                {
                    Instantiate(Fireball, new Vector2(Random.Range(LeftBorder, RightBorder), CeilHeight), Quaternion.identity);
                }
                yield return new WaitForSeconds(FireballDist);
            }
            yield return new WaitForSeconds(getInterval());
        }
    }

    private float getInterval()
    {
        return Interval - Delay - FireballDist * FireballRound;
    }
}
