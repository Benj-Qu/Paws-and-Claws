using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cableCar : MonoBehaviour
{
    public GameObject pl1;
    public GameObject pl2;
    public float moveSpeed;
    public float waitTime;
    public Transform[] movePos;

    private float waitNow;
    private int i;//定位现在的点
    private Transform playerDefTransform;
    private Rigidbody2D rb;

    private bool playeron = false;
    private bool enable = false;


    // Start is called before the first frame update
    void Start()
    {
        i = 1;
        waitNow = waitTime;
        playerDefTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (Vector2.Distance(transform.position, movePos[i].position) < 0.1f)
        {
            rb.velocity = Vector2.zero;
            if (waitNow < 0.0f)
            {
                Debug.Log(i);
                if (i == 1)
                {
                    pl1.GetComponent<PlayerController>().cableTrans();
                    pl2.GetComponent<PlayerController>().cableTrans();
                    this.transform.position = new Vector3(movePos[2].position.x, movePos[2].position.y, 0);
                    i = 3;
                } else if (i == 3)
                {
                    pl1.GetComponent<PlayerController>().cableTrans2();
                    pl2.GetComponent<PlayerController>().cableTrans2();
                    this.transform.position = new Vector3(movePos[0].position.x, movePos[0].position.y, 0);
                    i = 1;
                } else
                {
                    i += 1;
                }
                waitNow = waitTime;
            }
            else
            {
                waitNow -= Time.deltaTime;
            }
        }
        else
        {
            Vector2 dir = (movePos[i].position - transform.position);
            rb.velocity = dir.normalized * moveSpeed;
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        playeron = true;
    //    }
    //    if (collision.gameObject.CompareTag("trigger"))
    //    {
            
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        playeron = true;
    //    }
    //    if (collision.gameObject.CompareTag("trigger"))
    //    {
    //        playeron = true;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        playeron = false;
    //        collision.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        playeron = false;
    //        collision.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    //    }
    //}
}
