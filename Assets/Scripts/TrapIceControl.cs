using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapIceControl : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D bx;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        bx = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Collapse");
        }
    }

    void DisableBoxCollider()
    {
        bx.enabled = false;
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
