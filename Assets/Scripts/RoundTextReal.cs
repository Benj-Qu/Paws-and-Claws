using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundTextReal : MonoBehaviour
{
    Subscription<BigRoundIncEvent> round_inc_event_subscription;
    // private Animator _animator;
    // public TextMeshProUGUI text_;
    // private int animationHash;
    private Vector3 _oriPos;
    private Quaternion _oriQua;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Sprite _round1;
    private Sprite _round2;
    private Sprite _round3;

    private void Start()
    {
        _oriPos = gameObject.transform.position;
        _oriQua = gameObject.transform.rotation;
        round_inc_event_subscription = EventBus.Subscribe<BigRoundIncEvent>(OnRoundInc);
        // _animator = GetComponent<Animator>();
        // animationHash = Animator.StringToHash("Base Layer.RoundTextDrop");
        //Debug.Log("text_ is null" + text_);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _round1 = Resources.Load<Sprite>("Sprite/RoundTextWhite1");
        _round2 = Resources.Load<Sprite>("Sprite/RoundTextWhite2");
        _round3 = Resources.Load<Sprite>("Sprite/RoundTextWhite3");
    }

    private void OnRoundInc(BigRoundIncEvent e)
    {
        StartCoroutine(OtherHoldRoundAppear(e.round_big));
    }

    private IEnumerator OtherHoldRoundAppear(int round)
    {
        if (round == 1) _spriteRenderer.sprite = _round1;
        else if (round == 2) _spriteRenderer.sprite = _round2;
        else _spriteRenderer.sprite = _round3;
        gameObject.transform.position = _oriPos;
        gameObject.transform.rotation = _oriQua;
        _rb.velocity = Vector3.zero;

        //_rb.constraints = RigidbodyConstraints2D.None;
        //text_.text = "Round " + round.ToString();
        yield return new WaitForSeconds(4f);
        //text_.enabled = false;
        EventBus.Publish<RoundTextDoneEvent>(new RoundTextDoneEvent(round));
        // return to the original position
        //_rb.constraints = RigidbodyConstraints2D.FreezeAll;
        // gameObject.SetActive(false);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(round_inc_event_subscription);
    }
}
