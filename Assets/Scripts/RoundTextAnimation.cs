using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundTextAnimation : MonoBehaviour
{
    Subscription<BigRoundIncEvent> round_inc_event_subscription;
    private Animator _animator;
    public TextMeshProUGUI text_;
    private int animationHash;

    private void Start()
    {
        round_inc_event_subscription = EventBus.Subscribe<BigRoundIncEvent>(OnRoundInc);
        _animator = GetComponent<Animator>();
        animationHash = Animator.StringToHash("Base Layer.RoundTextDrop");
        Debug.Log("animator text" + animationHash);
    }

    private void OnRoundInc(BigRoundIncEvent e)
    {
        StartCoroutine(OtherHoldRoundAppear(e.round_big)); // TODO: temporarily on hold if used physical round text 
    }

    private IEnumerator OtherHoldRoundAppear(int round)
    {
        // _animator.SetLayerWeight(0, 1);
        // gameObject.SetActive(true);
        text_.enabled = true;
        text_.text = "Round " + round.ToString();
        _animator.Play(animationHash, 0, 0f);
        yield return new WaitForSeconds(1.4f);
        text_.enabled = false;
        EventBus.Publish<RoundTextDoneEvent>(new RoundTextDoneEvent(round));
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

public class RoundTextDoneEvent
{
    public int round_big;
    public RoundTextDoneEvent(int _round_big)
    {
        round_big = _round_big;
    }

    public override string ToString()
    {
        return "Round text finish display on big round " + round_big;
    }
}
