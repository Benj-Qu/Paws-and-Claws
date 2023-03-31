using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCloth : MonoBehaviour
{
    public PlayerController playerController;

    public Sprite armor;

    private bool invincible = false;

    public SpriteRenderer sr;

    private Sprite ori;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!invincible && playerController.GetInvincible())
        {
            invincible = true;
            ori = sr.sprite;
            sr.sprite = armor;
        }
        else if (invincible && !playerController.GetInvincible())
        {
            invincible = false;
            sr.sprite = ori;
        }
    }
}
