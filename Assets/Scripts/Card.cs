using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public string id;

    private Image _image;
    
    // Start is called before the first frame update
    void Start()
    {
        if (id == null)
        {
            id = "card_example";
        }
        
        // look for the corresponding image in the sprite folder
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
