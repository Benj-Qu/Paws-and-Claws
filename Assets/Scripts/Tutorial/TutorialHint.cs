using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialHint : MonoBehaviour
{
    public GameObject a1;
    public GameObject a2;
    public GameObject b2;
    public GameObject hint1;
    public GameObject hint2;
    public GameObject time;

    private void Start()
    {
        a1.SetActive(false);
        hint1.SetActive(false);
        a2.SetActive(false);
        b2.SetActive(false);
        hint2.SetActive(false);
    }

    private void Update()
    {
        if (GameController.instance.stage == 0)
        {
            a1.SetActive(true);
            hint1.SetActive(true);
        }
        else if (GameController.instance.stage == 1)
        {
            a1.SetActive(false);
            hint1.SetActive(false);
            a2.SetActive(true);
            b2.SetActive(true);
            hint2.SetActive(true);
            time.SetActive(false);
        }
        else if (GameController.instance.stage == 2)
        {
            a2.SetActive(false);
            b2.SetActive(false);
            hint2.SetActive(false);
            time.SetActive(true);
        }
    }
}
