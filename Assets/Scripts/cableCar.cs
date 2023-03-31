using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cableCar : MonoBehaviour
{
    public float moveSpeed;
    public float waitTime;
    public Transform[] movePos;

    private float waitNow;
    private int i;//定位现在的点
    private Transform playerDefTransform;
    private Rigidbody2D rb;


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
                if (i == 1)
                {
                    this.transform.position = new Vector3(movePos[2].position.x, movePos[2].position.y, 0);
                    i = 3;
                } else if (i == 3)
                {
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
