using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowAddScore : MonoBehaviour
{
    public GameObject flag;
    // public TextMeshProUGUI textObject;
    // private RectTransform textTransform;
    public GameObject textObject;

    public bool isMinus = false;
    // Start is called before the first frame update
    private Subscription<StartPartyTime> startPartyTimeEvent;
    

    private void Awake()
    {
        startPartyTimeEvent = EventBus.Subscribe<StartPartyTime>(onStartPartyTime);
    }

    void Start()
    {
        if (!isMinus) textObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Plus");
        textObject.SetActive(false);
        // textTransform = textObject.GetComponent<RectTransform>();
    }
    
    // // Update is called once per frame
    // void Update()
    // {
    //     
    // }

    private void onStartPartyTime(StartPartyTime e)
    {
        if (isMinus) return;
        if (e.ToString() == "Yes")
        {
            textObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Plus2");
        }
        else
        {
            textObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Plus");
        }
    }

    public void ShowScore()
    {
        textObject.SetActive(true);
        StartCoroutine(ShowScoreAnimation());
    }

    private IEnumerator ShowScoreAnimation()
    {
        // Vector2 position;
        // position.x = (flag.transform.position.x + 1) * 80;
        // position.y = flag.transform.position.y * 60;
        // textTransform.anchoredPosition = position;
        // textObject.transform.position = position;
        yield return new WaitForSeconds(0.2f);
        textObject.SetActive(false);
    }

    public void SetColor(int owner)
    {
        if (owner == 2)
        {
            textObject.GetComponent<SpriteRenderer>().color = Color.red;
        } else if (owner == 1)
        {
            textObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }
}
