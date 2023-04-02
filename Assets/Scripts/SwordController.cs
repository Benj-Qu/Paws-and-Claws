using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sp;
    private GameObject owner;

    public float Angular = 1000f;
    public float Range = 90f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.position = owner.transform.position;
    }

    public void Attack(GameObject player)
    {
        owner = player;
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        transform.eulerAngles += new Vector3(0f, 0f, Range / 2);
        yield return new WaitForSeconds(0.02f);
        sp.enabled = true;
        rb.angularVelocity = -Angular;
        yield return new WaitForSeconds(Range / Angular);
        Destroy(gameObject);
    }
}
