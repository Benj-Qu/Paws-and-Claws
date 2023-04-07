using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartyTime : MonoBehaviour
{
    public TextMeshProUGUI partyTimeText;
    public TextMeshProUGUI CountDownText;
    public GameObject TimeText;
    public flagController fc;
    public AudioClip count_down;
    public bool _changeColor = false;
    private TimeDisplayer timeDisplayer;
    // private GameObject _fill;
    // private Image _image;
    private float cnt_down = 10f;
    public bool countdownPlayed = false;
    private Subscription<GameOverEvent> gameover_event;

    private void Awake()
    {
        Debug.Log("party time canvas sub");
        gameover_event = EventBus.Subscribe<GameOverEvent>(OnGameOver);
    }

    private void OnGameOver(GameOverEvent e)
    {
        Debug.Log("text disable");
        partyTimeText.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        partyTimeText.enabled = false;
        CountDownText.enabled = false;
        countdownPlayed = false;
        timeDisplayer = TimeText.GetComponent<TimeDisplayer>();
        cnt_down = 10;
        // foreach (Transform child in progressBar.transform)
        // {
        //     if (child.gameObject.name == "Fill")
        //     {
        //         _fill = child.gameObject;
        //         _image = _fill.GetComponent<Image>();
        //         break;
        //     }
        // }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_slider.value);
        if (GameController.instance.stage == 2 && timeDisplayer.countdownTime <= 11)
        {
            if(timeDisplayer.countdownTime <= 10)
            {
                _changeColor = true;
                // _image.color = Color.red;
                if (GameController.instance.level != "Farm")
                {
                    partyTimeText.enabled = true;
                    fc.StartPartyTime();
                }
            }
            Debug.Log(cnt_down);
            if (Mathf.Abs(timeDisplayer.countdownTime - 11) <= 0.2f && !countdownPlayed)
            {
                Debug.Log("count == 10s");
                countdownPlayed = true;
                AudioSource partyTimecountDown = GetComponent<AudioSource>();
                if (partyTimecountDown) partyTimecountDown.PlayOneShot(count_down, 1f);
                CountDownText.text = "10";
                CountDownText.enabled = true;
                cnt_down = 10;
                EventBus.Publish<StartPartyTime>(new StartPartyTime(true));
            }
            if(Mathf.Abs(timeDisplayer.countdownTime - cnt_down) <= 0.2f)
            {
                CountDownText.text = (cnt_down - 1).ToString();
                Debug.Log(cnt_down.ToString());
                cnt_down = cnt_down - 1f;
                if(Mathf.Abs(cnt_down + 1) <= 0.2f)
                {
                    cnt_down = 10;
                    CountDownText.text = "10";
                    CountDownText.enabled = false;
                    partyTimeText.enabled = false;
                    EventBus.Publish<StartPartyTime>(new StartPartyTime(false));
                }
            }
        }

        if (_changeColor && timeDisplayer.countdownTime >= 44.5f)
        {
            _changeColor = false;
            // Color tempColor = _image.color;
            // tempColor = Color.blue;
            // _image.color = tempColor;
            partyTimeText.enabled = false;
            fc.EndPartyTime();
            cnt_down = 10;
            countdownPlayed = false;
        }
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(gameover_event);
    }
}

public class StartPartyTime
{
    private bool started = false;
    public StartPartyTime(bool _started)
    {
        started = _started;
    }

    public override string ToString()
    {
        if (started) return "Yes";
        return "No";
    }
}