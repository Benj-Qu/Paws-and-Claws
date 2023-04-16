using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sparkFollower : MonoBehaviour
{
    public GameObject flag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flag) transform.position = flag.transform.position;
    }
}
