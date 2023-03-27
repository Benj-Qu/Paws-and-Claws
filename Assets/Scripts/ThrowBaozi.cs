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
    public float MaxHorizontalSpeed = 2f;
    public float MinHorizontalSpeed = 1f;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (start == false && GameController.instance.stage == 2)
        {
            start = true;
            InvokeRepeating("baoziThrow", 2f, 1.5f);
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
        pos += new Vector3(0, 1f, 0);
        Debug.Log("HIIII");
        GameObject pump = Instantiate(Baozi, pos, Quaternion.identity);
        Rigidbody2D rgbd = pump.GetComponent<Rigidbody2D>();
        float v_x;
        do
        {
            v_x = Random.Range(-MaxHorizontalSpeed, MaxHorizontalSpeed);
        } while (v_x < MinHorizontalSpeed && v_x > -MinHorizontalSpeed);
        rgbd.velocity = new Vector2(v_x, SpeedUp);
    }
}
