using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPumpkin : MonoBehaviour
{
    public GameObject Pumpkin;
    public GameObject Fork;
    public bool left = true;
    public float RepeatRate = 1.5f;
    public float AttackDist = 1.5f;
    public bool isPenguin = false;

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
            InvokeRepeating("pumpkinThrow1", 2f, 2 * RepeatRate);
            InvokeRepeating("pumpkinThrow2", 2f + RepeatRate, 2 * RepeatRate);
        }

        if (start == true && GameController.instance.stage != 2)
        {
            start = false;
            CancelInvoke();
        }
    }

    void pumpkinThrow1()
    {
        if (isPenguin)
        {
            StartCoroutine(PenguinAttack1());
            return;
        }
        seed = Random.Range(0, 2);
        Vector3 pos = this.transform.position;
        pos += new Vector3(AttackDist, 0, 0);
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
        if (isPenguin)
        {
            StartCoroutine(PenguinAttack2());
            return;
        }
        seed = Random.Range(0, 2);
        Vector3 pos = this.transform.position;
        pos += new Vector3(-AttackDist, 0, 0);
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

    private IEnumerator PenguinAttack1()
    {
        Vector3 pos = this.transform.position;
        pos += new Vector3(AttackDist, 0, 0);
        GameObject pump;
        if (left == false)
        {
            left = !left;
            sr.flipX = !sr.flipX;
        }
        GetComponent<Animator>().SetTrigger("Attack");
        yield return new WaitForSeconds(15f / 60f);
        pump = Instantiate(Pumpkin, pos, Quaternion.identity);
        Rigidbody2D rgbd = pump.GetComponent<Rigidbody2D>();
        rgbd.velocity = new Vector2(speed, 0);
        rgbd.gravityScale = 0;
    }

    private IEnumerator PenguinAttack2()
    {
        Vector3 pos = this.transform.position;
        pos += new Vector3(-AttackDist, 0, 0);
        GameObject pump;
        if (left == true)
        {
            left = !left;
            sr.flipX = !sr.flipX;
        }
        GetComponent<Animator>().SetTrigger("Attack");
        yield return new WaitForSeconds(15f / 60f);
        Quaternion rotate = Quaternion.identity;
        rotate.eulerAngles += new Vector3(0, 180, 0);
        pump = Instantiate(Fork, pos, rotate);
        Rigidbody2D rgbd = pump.GetComponent<Rigidbody2D>();
        rgbd.velocity = new Vector2(-speed, 0);
        rgbd.gravityScale = 0;
    }
}
