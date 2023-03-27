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
    public GameObject bomb;

    public TextMeshProUGUI leftshiftText;
    public TextMeshProUGUI rightshiftText;
    public TextMeshProUGUI leftZText;
    public TextMeshProUGUI rightMText;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: change the logic according to tutorial name
        if (GameController.instance.level == "Winter Land" || GameController.instance.level == "Lantern Festival") // TODO: add more
        {
            akey.SetActive(false);
            dkey.SetActive(false);
            leftkey.SetActive(false);
            rightkey.SetActive(false);
            hintText.enabled = false;
            leftshift.SetActive(false);
            rightshift.SetActive(false);
            Z.SetActive(false);
            M.SetActive(false);
            bomb.SetActive(false);
            leftshiftText.enabled = false;
            rightshiftText.enabled = false;
            leftZText.enabled = false;
            rightMText.enabled = false;
        }
        if (GameController.instance.level == "Trial Test") // TODO: add more
        {
            akey.SetActive(false);
            dkey.SetActive(false);
            leftkey.SetActive(false);
            rightkey.SetActive(false);
            hintText.enabled = true;
            leftshift.SetActive(false);
            rightshift.SetActive(false);
            Z.SetActive(false);
            M.SetActive(false);
            bomb.SetActive(false);
            leftshiftText.enabled = false;
            rightshiftText.enabled = false;
            leftZText.enabled = false;
            rightMText.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (GameController.instance && GameController.instance.level == "Trial Test")
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
                hintText.enabled = true;
                hintText.text = "Seize the flag to gain points!";
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
                if (GameController.instance && GameController.instance.level == "Trial Test")
                {
                    hintText.text = "   can destroy any block.";
                }
                else
                {
                    hintText.enabled = false;
                }
            }
            else
            {
                if (GameController.instance && GameController.instance.level == "Trial Test")
                {
                    hintText.text = "Select a block, give the opponent the other.";
                }
                else
                {
                    hintText.enabled = true;
                }
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
                if (GameController.instance && GameController.instance.level == "Trial Test")
                {
                    hintText.text = "Select a block, give the opponent the other.";
                } else
                {
                    hintText.enabled = true;
                }    
            }
            else
            {
                if (GameController.instance && GameController.instance.level == "Trial Test")
                {
                    hintText.text = "   can destroy any block.";
                }
                else
                {
                    hintText.enabled = false;
                }
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
                bomb.SetActive(false);
                leftshiftText.enabled = false;
                rightshiftText.enabled = false;
                leftZText.enabled = false;
                rightMText.enabled = false;
            }
            else
            {
                leftshiftText.enabled = true;
                rightshiftText.enabled = true;
                leftZText.enabled = true;
                rightMText.enabled = true;
            }
        }
        else
        {
            if (shift_appear)
            {
                leftshift.SetActive(true);
                rightshift.SetActive(true);
                Z.SetActive(true);
                M.SetActive(true);
                bomb.SetActive(true);
                leftshiftText.enabled = true;
                rightshiftText.enabled = true;
                leftZText.enabled = true;
                rightMText.enabled = true;
            }
            else
            {
                leftshiftText.enabled = false;
                rightshiftText.enabled = false;
                leftZText.enabled = false;
                rightMText.enabled = false;
            }
        }
    }
}
