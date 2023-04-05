using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    // indicate card category
    public int block_id = -1; 
    public int index = -1;
    public Animator animator;
    public bool playDescriptionAnimation = false;
    public float timeBeforeCardStaticBecomeAnimation = 0.35f;
    
    private Image _image;
    private bool move = false;
    private Vector3 target;
    private Vector3 speed;
    private bool _inSelection = true;
    private RuntimeAnimatorController _runtimeAnimatorController;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        SetCard(block_id);
    }

    // Update is called once per frame
    private void Update()
    {
        if (move)
        {
            Vector3 currentCameraPos = _image.transform.position;
            currentCameraPos += speed * Time.deltaTime;
            _image.transform.position = currentCameraPos;
        }
    }

    public void SetCard(int _block_id)
    {
        Reset();
        if (block_id == -1)
        {
            block_id = 0;
        }

        // set the block id
        this.block_id = _block_id;
        
        // look for the corresponding image in the sprite folder
        var spritePath = "Sprite/" + AllCards.cards[this.block_id] + "_";
        _image.sprite = Resources.Load<Sprite>(spritePath);

        // look for the corresponding animation for this card
        var animatorPath = "Animations/DescriptionAnimation/" + AllCards.cards[this.block_id];
        _runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(animatorPath);
    }

    public void StartSelectEffect(int leftOrRight, Vector3 pos)
    {
        Vector3 img_pos = _image.transform.position;
        _image.transform.position = pos;
        _image.transform.localScale *= 2;
        StartCoroutine(MoveToPos(img_pos, leftOrRight));
    }

    // pos is the target
    private IEnumerator MoveToPos(Vector3 img_pos, int leftOrRight)
    {
        move = true;
        target = img_pos;
        speed = (target - _image.transform.position) / 0.75f;
        
        yield return new WaitForSeconds(0.75f);
        
        move = false;
        _image.transform.localScale /= 2;
        _image.transform.position = img_pos;
    }

    public void SetIndex(int _index)
    {
        index = _index;
    }

    public void SetIndexAndWhichCard(int _block_id, int _index)
    {
        SetCard(_block_id);
        SetIndex(_index);
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

    public void SetCardBrightness(float brightness)
    {
        Color c = _image.color;
        c.a = brightness;
        _image.color = c;
    }

    public void PlayAnimationIfNotPlaying()
    {
        if (_inSelection && playDescriptionAnimation == false)
        {
            playDescriptionAnimation = true;
            // set up animator
            StartCoroutine(WaitPlayAnimation());
        }
    }

    private IEnumerator WaitPlayAnimation()
    {
        yield return new WaitForSeconds(timeBeforeCardStaticBecomeAnimation);
        if (playDescriptionAnimation)
        {
            animator.runtimeAnimatorController = _runtimeAnimatorController;
        }
    }

    public void StopPlayAnimationIfPlaying()
    {
        if (_inSelection && playDescriptionAnimation)
        {
            playDescriptionAnimation = false;
            // set animator controller to None
            animator.runtimeAnimatorController = null;
        }
    }

    public void SetInSelectionParameter(bool inselection)
    {
        _inSelection = inselection;
    }

    // run this function before the SetCard is called
    public void Reset()
    {
        _runtimeAnimatorController = null;
        animator.runtimeAnimatorController = _runtimeAnimatorController;
        _inSelection = true;
        playDescriptionAnimation = false;
    }
}
