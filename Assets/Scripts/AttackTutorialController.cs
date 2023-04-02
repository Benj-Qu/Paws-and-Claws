using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AttackTutorialController : MonoBehaviour
{
    public PlayerController p1;
    public PlayerController p2;
    public TextMeshProUGUI CntDownText;
    public GameObject blocks;
    bool first = true;
    float AttackTime = 10f;
    int TimeCntDown = 10;
    // Start is called before the first frame update
    void Start()
    {
        CntDownText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(AttackTime) <= 0.2f)
        {
            GameController.instance.attackTutorialFinished = true;
        }
        if(p1.isAttacked() && p2.isAttacked())
        {
            if (first)
            {
                first = false;
                CntDownText.enabled = true;
                for(int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                }
            }
            AttackTime -= Time.deltaTime;
            if(Mathf.Abs(AttackTime - TimeCntDown) <= 0.5f)
            {
                CntDownText.text = TimeCntDown.ToString();
                TimeCntDown -= 1;
            }
        }
    }
}
