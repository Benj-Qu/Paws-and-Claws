using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseFollower : MonoBehaviour
{
    public bool is_dog;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (is_dog)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x + 0.5f, mousePos.y - 0.3f, -5);
        }
        else
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x + 1f, mousePos.y - 0.3f, -5);
        }

    }
}
