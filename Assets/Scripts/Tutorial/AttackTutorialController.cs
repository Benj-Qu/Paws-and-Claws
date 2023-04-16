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
    public GameObject player1_win_text;
    public GameObject player2_win_text;
    public TextMeshProUGUI FreeAttackText;
    public GameObject flag;
    bool first = true;
    float AttackTime = 10f;
    int TimeCntDown = 10;
    bool p1_attacked = false;
    bool p2_attacked = false;
    // Start is called before the first frame update
    void Start()
    {
        CntDownText.enabled = false;
        FreeAttackText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(AttackTime) <= 0.2f)
        {
            GameController.instance.attackTutorialFinished = true;
            CntDownText.enabled = false;
        }
        if (p1.isAttacked()) {
            p1_attacked = true;
            player2_win_text.SetActive(true);
        }

        if (p2.isAttacked())
        {
            p2_attacked = true;
            player1_win_text.SetActive(true);
        }
        if(p1_attacked && p2_attacked)
        {
            if (first)
            {
                FreeAttackText.enabled = true;
                first = false;
                CntDownText.enabled = true;
                flag.SetActive(true);
                //for(int i = 0; i < transform.childCount; i++)
                //{
                //    transform.GetChild(i).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                //}
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
