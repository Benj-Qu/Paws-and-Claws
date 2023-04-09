using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwBaoziHelper : MonoBehaviour
{
    public GameObject steam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void finish()
    {
        steam.GetComponent<ThrowBaozi>().baothrow();
    }
}
