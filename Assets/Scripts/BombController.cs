using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    private GameObject explosionAes;
    private bool explosion = false;
    private bool start = false; // battle begin

    // Start is called before the first frame update
    void Start()
    {
        explosionAes = Resources.Load<GameObject>("Prefab/Explosion");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("stage" + GameController.instance.stage);
        if (start == false && GameController.instance.stage == 2)
        {
            start = true;
            if (explosion == false)
            {
                explosion = true;
                // StartCoroutine(WaitAndExplode());
                Instantiate(GameController.instance.explosionAes, gameObject.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }

    private IEnumerator WaitAndExplode()
    {
        yield return new WaitForSeconds(10f);
        
        Instantiate(GameController.instance.explosionAes, gameObject.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collide" + collision.gameObject);
        
        Debug.Log("start" + start);
        if (collision.gameObject.CompareTag("Block") && start)
        {
            
            Debug.Log("collide in" );
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
    
    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("collide1" + collision.gameObject);
        Debug.Log("start1" + start);
        if (collision.gameObject.CompareTag("Block") && start)
        {
            Debug.Log("collide1 in" );
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
