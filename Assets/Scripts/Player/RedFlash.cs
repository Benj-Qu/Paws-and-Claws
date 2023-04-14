using System.Collections;
using UnityEngine;

public class RedFlash : MonoBehaviour
{
    private SpriteRenderer sr;
    public bool flashing;

    private void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        flashing = true;
        StartCoroutine(flash());
    }

    private void OnDisable()
    {
        stop();
    }

    private IEnumerator flash()
    {
        bool red = true;
        while (flashing)
        {
            if (red)
            {
                sr.color = Color.red;
            }
            else
            {
                sr.color = Color.white;
            }
            red = !red;
            yield return new WaitForSeconds(0.1f);
        }
        sr.color = Color.white;
    }

    public void stop()
    {
        if (sr)
        {
            flashing = false;
            sr.color = Color.white;
            StopAllCoroutines();
            sr.color = Color.white;
        }
    }
}
