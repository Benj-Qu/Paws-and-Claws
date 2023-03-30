using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class WinImage : MonoBehaviour
{
    public GameObject paw;

    public GameObject claw;

    public GameObject Cat;

    public GameObject Dog;

    private Image _image;

    public GameObject vs;

    public TextMeshProUGUI text_;
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        vs.SetActive(false);
        text_.enabled = false;
        _image.enabled = false;
        Dog.SetActive(false);
        Cat.SetActive(false);
        paw.SetActive(false);
        claw.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CatWin()
    {
        _image.sprite = Resources.Load<Sprite>("Background/IMG_1945");
        _image.enabled = true;
        text_.enabled = true;
        Cat.SetActive(true);
        claw.SetActive(true);
    }

    public void DogWin()
    {
        _image.sprite = Resources.Load<Sprite>("Background/IMG_1944");
        _image.enabled = true;
        text_.enabled = true;
        Dog.SetActive(true);
        paw.SetActive(true);
    }

    public void Tie()
    {
        VS();
    }

    public void VS()
    {
        _image.sprite = Resources.Load<Sprite>("Background/IMG_0060");
        _image.enabled = true;
        text_.enabled = true;
        vs.SetActive(true);
        Dog.SetActive(true);
        Cat.SetActive(true);
        paw.SetActive(true);
        claw.SetActive(true);
    }

    public void reset()
    {
        _image.enabled = false;
        Dog.SetActive(false);
        text_.enabled = false;
        vs.SetActive(false);
        Cat.SetActive(false);
        paw.SetActive(false);
        claw.SetActive(false);
    }
}
