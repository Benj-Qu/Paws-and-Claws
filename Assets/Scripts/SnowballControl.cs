using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballControl : MonoBehaviour
{

    private Rigidbody2D rb;
    private float speed = 500;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        Destroy(this, 9f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MoveRotation(rb.rotation + speed * Time.fixedDeltaTime);
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
