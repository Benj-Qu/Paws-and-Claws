using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialSelectionHint : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {

        appear = true;
        shift_appear = false;


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
                }
                else
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
