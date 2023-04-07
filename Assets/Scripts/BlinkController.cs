using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkController : MonoBehaviour
{
    float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Color tmp = GetComponent<SpriteRenderer>().color;
        time += Time.deltaTime;
        if(Mathf.Abs(time - 0.6f) <= 0.1f)
        {
            if (tmp.a == 1f)
            {
                tmp.a = 0f;
            }
            else
            {
                tmp.a = 1f;
            }
            GetComponent<SpriteRenderer>().color = tmp;
            time = 0f;
        }
    }
}
