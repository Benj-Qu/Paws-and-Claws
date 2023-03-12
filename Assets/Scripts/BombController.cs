using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    private GameObject explosionAes;
    private bool explosion = false;
    
    // Start is called before the first frame update
    void Start()
    {
        explosionAes = Resources.Load<GameObject>("Prefab/Explosion");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
            blockMovement bm1 = collision.gameObject.GetComponent<blockMovement>();
            blockMovement bm2 = gameObject.GetComponent<blockMovement>();
            if (bm1 && bm2 && bm1.set && bm2.set)
            {
                if (explosion == false)
                {
                    Instantiate(GameController.instance.explosionAes, gameObject.transform.position, Quaternion.identity);
                    explosion = true;
                }
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
    
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
            blockMovement bm1 = collision.gameObject.GetComponent<blockMovement>();
            blockMovement bm2 = gameObject.GetComponent<blockMovement>();
            if (bm1 && bm2 && bm1.set && bm2.set) 
            {
                if (explosion == false)
                {
                    Instantiate(GameController.instance.explosionAes, gameObject.transform.position, Quaternion.identity);
                    explosion = true;
                }
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
