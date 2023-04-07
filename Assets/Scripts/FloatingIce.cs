using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingIce : MonoBehaviour
{
    public float moveSpeed;
    public float waitTime;
    public Transform[] movePos;
    public float wholetime = 6f;

    private float currtime = 0;
    private float waitNow;
    private int i;//定位现在的点
    private Transform playerDefTransform;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        i = 1;
        waitNow = waitTime;
        currtime = wholetime;
        playerDefTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        currtime += Time.deltaTime;
        if (Vector2.Distance(transform.position, movePos[i].position) < 0.1f || currtime > wholetime)
        {
            rb.velocity = Vector2.zero;
            if (waitNow < 0.0f || currtime > wholetime)
            {
                if (i == 0)
                {
                    i = 1;
                } else
                {
                    i = 0;
                }
                currtime = 0;
                waitNow = waitTime;
            } else
            {
                waitNow -= Time.deltaTime;
            }
        } else
        {
            Vector2 dir = (movePos[i].position - transform.position).normalized;
            rb.velocity = dir * moveSpeed;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        collision.gameObject.transform.parent = this.gameObject.transform;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        collision.gameObject.transform.parent = playerDefTransform;
    //    }
    //}
}