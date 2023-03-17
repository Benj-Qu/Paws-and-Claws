using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectionHint : MonoBehaviour
{
    public bool appear;
    public bool shift_appear;

    public GameObject akey;
    public GameObject dkey;
    public GameObject leftkey;
    public GameObject rightkey;

    public TextMeshProUGUI hintText;

    public GameObject leftshift;
    public GameObject rightshift;

    public GameObject Z;
    public GameObject M;

    public TextMeshProUGUI leftshiftText;
    public TextMeshProUGUI rightshiftText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance && GameController.instance.level == "Tutorial")
        {
            if (GameController.instance.stage == 0)
            {
                appear = true;
                shift_appear = false;
            }
            else if (GameController.instance.stage == 1)
            {
                appear = false;
                shift_appear = true;
            }
            else
            {
                appear = false;
                shift_appear = false;
            }
        }
        else
        {
            shift_appear = false;
            appear = false;
        }

        if (akey.activeSelf)
        {
            if (!appear)
            {
                akey.SetActive(false);
                dkey.SetActive(false);
                leftkey.SetActive(false);
                rightkey.SetActive(false);
                hintText.enabled = false;
            }
            else
            {
                hintText.enabled = true;
            }
        }
        else
        {
            if (appear)
            {
                akey.SetActive(true);
                dkey.SetActive(true);
                leftkey.SetActive(true);
                rightkey.SetActive(true);
                hintText.enabled = true;
            }
            else
            {
                hintText.enabled = false;
            }
        }

        if (leftshift.activeSelf)
        {
            if (!shift_appear)
            {
                leftshift.SetActive(false);
                rightshift.SetActive(false);
                Z.SetActive(false);
                M.SetActive(false);
                leftshiftText.enabled = false;
                rightshiftText.enabled = false;
            }
            else
            {
                leftshiftText.enabled = true;
                rightshiftText.enabled = true;
            }
        }
        else
        {
            if (shift_appear)
            {
                leftshift.SetActive(true);
                rightshift.SetActive(true);
                Z.SetActive(true);
                Z.SetActive(false);
                leftshiftText.enabled = true;
                rightshiftText.enabled = true;
            }
            else
            {
                leftshiftText.enabled = false;
                rightshiftText.enabled = false;
            }
        }
    }
}
