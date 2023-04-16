using System.Collections;
using UnityEngine;

public class RedFlash : MonoBehaviour
{
    public SpriteRenderer sr;
    public bool flashing;

    private void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        flashing = true;
        StartCoroutine(flash());
    }

    private IEnumerator flash()
    {
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
        Debug.Log("Stopping rf");
        if (sr)
        {
            flashing = false;
            sr.color = Color.white;
            StopAllCoroutines();
        }
    }
}
