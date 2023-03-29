using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempAnimationPlus30 : MonoBehaviour
{
    public GameObject thirty;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        thirty.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, thirty.transform.position) < 2.1f)
        {
            if (!thirty.activeSelf) thirty.SetActive(true);
        }
        else
        {
            if (thirty.activeSelf) thirty.SetActive(false);
        }
    }
}
