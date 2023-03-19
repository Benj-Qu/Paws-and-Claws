using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartyTime : MonoBehaviour
{
    public TextMeshProUGUI partyTimeText;
    public GameObject progressBar;
    public bool _changeColor = false;
    private Slider _slider;
    private GameObject _fill;
    private Image _image;
    
    // Start is called before the first frame update
    void Start()
    {
        partyTimeText.enabled = false;
        _slider = progressBar.GetComponent<Slider>();
        foreach (Transform child in progressBar.transform)
        {
            if (child.gameObject.name == "Fill")
            {
                _fill = child.gameObject;
                _image = _fill.GetComponent<Image>();
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_changeColor == false && GameController.instance.stage == 2 && GameController.instance.level != "Tutorial" &&
            GameController.instance.level != "Tutorial1" && GameController.instance.level != "Trial Level" && _slider.value <= 10)
        {
            _changeColor = true;
            Color tempColor = _image.color;
            tempColor = Color.red;
            _image.color = tempColor;
            partyTimeText.enabled = true;
        }

        if (_changeColor && _slider.value >= 50f)
        {
            _changeColor = false;
            Color tempColor = _image.color;
            tempColor = Color.blue;
            _image.color = tempColor;
            partyTimeText.enabled = false;
        }
    }
}
