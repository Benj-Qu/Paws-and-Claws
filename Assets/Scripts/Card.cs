using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int block_id = -1;

    private Image _image;
    
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        
        SetCard(block_id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCard(int block_id)
    {
        if (block_id == -1)
        {
            block_id = 0;
        }
        
        // set the block id
        this.block_id = block_id;
        
        // look for the corresponding image in the sprite folder
        _image.sprite = Resources.Load<Sprite>("Sprite/" + AllCards.cards[this.block_id]);
    }

    public void CardDisappear()
    {
        Color c = _image.color;
        c.a = 0;
        _image.color = c;
    }

    public void CardAppear()
    {
        Color c = _image.color;
        c.a = 1;
        _image.color = c;
    }
}
