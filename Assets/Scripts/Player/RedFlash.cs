using System.Collections;
using UnityEngine;

public class RedFlash : MonoBehaviour
{
    public SpriteRenderer sr;
    public bool flashing;
    public IEnumerator coroutine;

    private void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        coroutine = flash();
        StartCoroutine(coroutine);
    }

    private void OnDisable()
    {
        stop();
    }

    private IEnumerator flash()
    {
        flashing = true;
        bool red = true;
        while (flashing && sr)
        {
            Debug.Log("Flashing");
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
    }

    public void stop()
    {
        if (sr && flashing)
        {
            Debug.Log("Stopping rf " + gameObject.name);
            flashing = false;
            sr.color = Color.white;
            StopCoroutine(coroutine);
        }
    }
}
