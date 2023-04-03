using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCloth : MonoBehaviour
{
    public PlayerController playerController;

    public GameObject armor;
    public GameObject shield;

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
            armor.SetActive(true);
            shield.SetActive(true);
        }
        else if (invincible && !playerController.GetInvincible())
        {
            invincible = false;
            armor.SetActive(false);
            armor.SetActive(false);
        }
    }
}
