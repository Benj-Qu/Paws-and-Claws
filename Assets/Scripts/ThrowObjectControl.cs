using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObjectControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Flag") && !collision.gameObject.CompareTag("Box"))
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name != "Snowman" && collision.gameObject.name != "panda")
        {

            if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Flag") && !collision.gameObject.CompareTag("Box"))
            {
                Destroy(this.gameObject);
            }

            if (collision.gameObject.CompareTag("Wall"))
            {
                Destroy(this.gameObject);
            }
        }
    }


}
