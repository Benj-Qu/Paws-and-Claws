using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPumpkin : MonoBehaviour
{
    public GameObject Pumpkin;

    private float speed = 3;
    // Start is called before the first frame update

    void Start()
    {
        InvokeRepeating("pumpkinThrow", 2f, 3f);
        InvokeRepeating("pumpkinThrow2", 3.5f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void pumpkinThrow()
    {
        Vector3 pos = this.transform.position;
        pos += new Vector3(0, 1.5f, 0);
        GameObject pump = Instantiate(Pumpkin, pos, Quaternion.identity);

        Rigidbody2D rgbd = pump.GetComponent<Rigidbody2D>();
        rgbd.velocity = new Vector2(speed, 0);
        rgbd.gravityScale = 0;
        Destroy(pump, 5f);
    }

    void pumpkinThrow2()
    {
        Vector3 pos = this.transform.position;
        pos += new Vector3(0, 1.5f, 0);
        GameObject pump = Instantiate(Pumpkin, pos, Quaternion.identity);

        Rigidbody2D rgbd = pump.GetComponent<Rigidbody2D>();
        rgbd.velocity = new Vector2(-speed, 0);
        rgbd.gravityScale = 0;
        Destroy(pump, 5f);
    }
}
