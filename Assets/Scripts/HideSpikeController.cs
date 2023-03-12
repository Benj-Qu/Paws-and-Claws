using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSpikeController : MonoBehaviour
{
    public GameObject SpikeBox;
    public float time;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SpikeAttack());
        }
    }

    IEnumerator SpikeAttack()
    {
        yield return new WaitForSeconds(time);
        anim.SetTrigger("attack");
        Instantiate(SpikeBox, transform.position, Quaternion.identity);
    }
}
