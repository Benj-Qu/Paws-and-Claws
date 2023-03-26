using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBaozi : MonoBehaviour
{
    public GameObject Baozi;

    private int seed;
    private bool start = false;
    // Start is called before the first frame update

    public float SpeedUp = 3f;
    public float verticalSpeed = 2f;

    void Start()
    {
        InvokeRepeating("baoziThrow", 2f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (start == false && GameController.instance.stage == 2)
        {
            start = true;
            InvokeRepeating("baoziThrow", 2f, 3f);
        }

        if (start == true && GameController.instance.stage != 2)
        {
            start = false;
            CancelInvoke();
        }
    }

    void baoziThrow()
    {
        Vector3 pos = this.transform.position;
        pos += new Vector3(0, 1.5f, 0);
        Debug.Log("HIIII");
        GameObject pump = Instantiate(Baozi, pos, Quaternion.identity);
        Rigidbody2D rgbd = pump.GetComponent<Rigidbody2D>();
        rgbd.velocity = new Vector2(Random.Range(-verticalSpeed, verticalSpeed), SpeedUp);
    }
}
