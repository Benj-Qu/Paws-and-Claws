using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lantern : MonoBehaviour
{
    private Rigidbody2D _rb;

    //private bool freeze = false;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!freeze && GameController.instance.stage == 1)
        {
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            freeze = true;
        }
        else
        {
            if (freeze)
            {
                _rb.constraints = RigidbodyConstraints2D.None;
                freeze = false;
            }
        }*/
    }
}
