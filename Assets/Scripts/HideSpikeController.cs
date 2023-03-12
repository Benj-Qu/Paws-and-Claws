using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSpikeController : MonoBehaviour
{
    public GameObject SpikeBox;
    public float time=0.4f;

    private Animator anim;
    private bool start;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (start == false && GameController.instance.stage == 2)
        {
            start = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (start == true && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SpikeAttack());
        }
    }

    IEnumerator SpikeAttack()
    {
        yield return new WaitForSeconds(time);
        anim.SetTrigger("attack");
        yield return new WaitForSeconds(1f);
        //Instantiate(SpikeBox, transform.position, Quaternion.identity);
        SpikeBox.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        SpikeBox.SetActive(false);
    }
}
