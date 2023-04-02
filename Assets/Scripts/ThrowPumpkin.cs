using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPumpkin : MonoBehaviour
{
    public GameObject Pumpkin;
    public GameObject Fork;
    public bool left = true;

    private float speed = 4;
    private int seed;
    private bool start = false;
    private SpriteRenderer sr;
    // Start is called before the first frame update

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        //InvokeRepeating("pumpkinThrow", 2f, 3f);
        //InvokeRepeating("pumpkinThrow2", 3.5f, 3f);
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

        if (start == true && GameController.instance.stage != 2)
        {
            start = false;
            CancelInvoke();
        }
    }

    void pumpkinThrow()
    {
        seed = Random.Range(0, 2);
        Vector3 pos = this.transform.position;
        pos += new Vector3(1.5f, 0, 0);
        GameObject pump;
        if (left == false)
        {
            left = !left;
            sr.flipX = !sr.flipX;
        }
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
        pos += new Vector3(-1.5f, 0, 0);
        GameObject pump;
        if (left == true)
        {
            left = !left;
            sr.flipX = !sr.flipX;
        }
        if (seed == 1)
        {
            Quaternion rotate = Quaternion.identity;
            rotate.eulerAngles += new Vector3(0, 180, 0);
            pump = Instantiate(Pumpkin, pos, rotate);
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
