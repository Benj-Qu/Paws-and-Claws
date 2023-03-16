using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPumpkin : MonoBehaviour
{
    public GameObject Pumpkin;
    public GameObject Fork;

    private float speed = 4;
    private int seed;
    private bool start = false;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (start == false && GameController.instance.stage == 2)
        {
            start = true;
            InvokeRepeating("pumpkinThrow", 2f, 3f);
            InvokeRepeating("pumpkinThrow2", 3.5f, 3f);
        }

        if (start == true && GameController.instance.stage == 1)
        {
            CancelInvoke();
        }
    }

    void pumpkinThrow()
    {
        seed = Random.Range(0, 2);
        Vector3 pos = this.transform.position;
        //pos += new Vector3(0, 1.5f, 0);
        GameObject pump;
        if (seed == 1)
        {
            pump = Instantiate(Pumpkin, pos, Quaternion.identity);
        } else
        {
            pump = Instantiate(Fork, pos, Quaternion.identity);
        }

        Rigidbody2D rgbd = pump.GetComponent<Rigidbody2D>();
        rgbd.velocity = new Vector2(speed, 0);
        rgbd.gravityScale = 0;
    }

    void pumpkinThrow2()
    {
        seed = Random.Range(0, 2);
        Vector3 pos = this.transform.position;
        //pos += new Vector3(0, 1.5f, 0);
        GameObject pump;
        if (seed == 1)
        {
            pump = Instantiate(Pumpkin, pos, Quaternion.identity);
        }
        else
        {
            Quaternion rotate = Quaternion.identity;
            rotate.eulerAngles += new Vector3(0, 180, 0);
            pump = Instantiate(Fork, pos, rotate);
        }

        Rigidbody2D rgbd = pump.GetComponent<Rigidbody2D>();
        rgbd.velocity = new Vector2(-speed, 0);
        rgbd.gravityScale = 0;
    }
}
