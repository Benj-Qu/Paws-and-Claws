using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armorControl : MonoBehaviour
{
    public int dogOrCat;

    public GameObject shield;

    public PlayerController player;

    public SpriteRenderer armorsr;

    public SpriteRenderer shieldsr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dogOrCat == 1)
        {
            if (player.GetFlip())
            {
                armorsr.flipX = player.GetFlip();
                shieldsr.flipX = player.GetFlip();
                Vector3 temp = new Vector3(-0.13f, 0.05f, 0f);
                Vector3 temp1 = new Vector3(-0.81f, -1.02f, 0f);
                gameObject.transform.localPosition = temp;
                shield.transform.localPosition = temp1;
            }
            else
            {
                armorsr.flipX = player.GetFlip();
                shieldsr.flipX = player.GetFlip();
                Vector3 temp = new Vector3(0.08f, 0.03f, 0f);
                Vector3 temp1 = new Vector3(0.84f, -1.06f, 0f);
                gameObject.transform.localPosition = temp;
                shield.transform.localPosition = temp1;
            }
        }
        else
        {
            if (player.GetFlip())
            {
                armorsr.flipX = player.GetFlip();
                shieldsr.flipX = player.GetFlip();
                Vector3 temp = new Vector3(-0.08f, -0.08f, 0f);
                Vector3 temp1 = new Vector3(-0.94f, -1.16f, 0f);
                gameObject.transform.localPosition = temp;
                shield.transform.localPosition = temp1;
            }
            else
            {
                armorsr.flipX = player.GetFlip();
                shieldsr.flipX = player.GetFlip();
                Vector3 temp = new Vector3(0.08f, -0.08f, 0f);
                Vector3 temp1 = new Vector3(0.88f, -1.15f, 0f);
                gameObject.transform.localPosition = temp;
                shield.transform.localPosition = temp1;
            }
        }
    }
}
