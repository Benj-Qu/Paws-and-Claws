using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StoryController : MonoBehaviour
{
    public GameObject story1;
    public GameObject story2;
    public GameObject story3;
    public GameObject story4;
    public GameObject story_cat;
    public GameObject story_dog;
    public GameObject mask;
    public GameObject story5;
    public GameObject scarecrow;
    public TextMeshProUGUI hint;
    public GameObject A;
    public GameObject loadingManager;

    public AudioSource storyAS;
    public AudioClip storyClip;
    public AudioSource birdAS;
    public AudioClip birdClip;
    public AudioSource thunderAS;
    public AudioClip thunderClip;
    public AudioSource bgmAS;
    public AudioClip bgmClip;
    public float volume = 1f;

    public Animator story1_animator;
    public Text text;
    private Queue<string> scripts = new Queue<string>();
    public int story_stage = 1;
    private bool skip = false;

    // Start is called before the first frame update
    void Start()
    {
        story1_animator.speed = 0.0f;
        StartCoroutine(Story1Coroutine());
        scripts.Enqueue("[speed=0.08]Once upon a time, there was a magic land,\nwhere cats and dogs lived together happily.");
        scripts.Enqueue("[speed=0.08]However, one day, a dark shadow loomed over the land,\n threatening to destroy their peaceful existence.");
        scripts.Enqueue("[speed=0.08]Ever since then, a Guardian would be selected\nevery year to protect the magic land.");
        scripts.Enqueue("[speed=0.08]This year, the competition is fierce between\n<size=30>DOGS</size> and <size=30>CATS</size>!");
        scripts.Enqueue("[speed=0.08]Ready to help DOGS or CATS become the next guardian?\n Start from the center of magic land!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("A1") || Input.GetButtonDown("A2"))
        {
            if(story_stage < 5)
            {
                skip = true;
                Debug.Log("Stop coroutine 1");
                StopCoroutine("Story1Coroutine");
                Debug.Log("Stop coroutine 2");
                StopCoroutine("Story2Coroutine");
                Debug.Log("Stop coroutine 3");
                StopCoroutine("Story3Coroutine");
                Debug.Log("Stop coroutine 4");
                StopCoroutine("Story4Coroutine");
                text.SkipTypeText();
                storyAS.Stop();
                birdAS.Stop();
                Debug.Log("Inactive storys");
                story1.SetActive(false);
                story2.SetActive(false);
                story3.SetActive(false);
                story4.SetActive(false);
                if (!bgmAS.isPlaying)
                {
                    bgmAS.Play();
                }
                story_stage = 5;
                while (scripts.Count > 1)
                {
                    scripts.Dequeue();
                }
                StartCoroutine(Story5Coroutine());
            }
            if(story_stage == 6)
            {
                loadingManager.GetComponent<Michsky.LSS.LoadingScreenManager>().LoadScene("NewIntro");
            }
        }
    }

    private void ShowScript()
    {
        Debug.Log(scripts.Count);
        if (scripts.Count <= 0)
        {
            return;
        }
        storyAS.Play();
        text.TypeText(scripts.Dequeue(), onComplete: () => {
            storyAS.Stop();
            if (story_stage == 5)
            {
                story_stage++;
            }
            if (!skip && story_stage < 5)
            {
                story_stage++;
                if (story_stage == 2)
                {
                    Debug.Log("Start coroutine 2");
                    StartCoroutine("Story2Coroutine");
                }
                if (story_stage == 3)
                {
                    Debug.Log("Start coroutine 3");
                    StartCoroutine("Story3Coroutine");
                }
                if (story_stage == 4)
                {
                    Debug.Log("Start coroutine 4");
                    StartCoroutine("Story4Coroutine");
                }
                if (story_stage == 5)
                {
                    Debug.Log("Start coroutine 5");
                    StartCoroutine("Story5Coroutine");
                }
            }
        });

    }

    private IEnumerator Story1Coroutine()
    {
        birdAS.Play();
        yield return new WaitForSeconds(5.0f);
        ShowScript();
        story1_animator.speed = 1.0f;
        Debug.Log("coroutine1 end");
    }

    private IEnumerator Story2Coroutine()
    {
        yield return new WaitForSeconds(1.5f);
        birdAS.Stop();
        ShowScript();
        yield return new WaitForSeconds(2f);
        Debug.Log("active story2");
        story2.SetActive(true);
        thunderAS.Play();
        Debug.Log("coroutine2 end");
    }

    private IEnumerator Story3Coroutine()
    {
        yield return new WaitForSeconds(2f);
        story1.SetActive(false);
        story2.SetActive(false);
        story3.SetActive(true);
        if (!bgmAS.isPlaying)
        {
            bgmAS.Play();
        }
        Vector3 temp = text.rectTransform.anchoredPosition;
        temp.y = -600;
        text.rectTransform.anchoredPosition = temp;
        text.color = Color.white;
        ShowScript();
        Debug.Log("coroutine3 end");
    }

    private IEnumerator Story4Coroutine()
    {
        yield return new WaitForSeconds(2f);
        story3.SetActive(false);
        Color tmp = Color.black;
        tmp.a = 0.7f;
        mask.GetComponent<SpriteRenderer>().color = tmp;
        story4.SetActive(true);
        Vector3 temp = text.rectTransform.anchoredPosition;
        temp.y = -300;
        text.rectTransform.anchoredPosition = temp;
        text.color = Color.white;
        ShowScript();
        yield return new WaitForSeconds(3f);
        story_dog.SetActive(true);
        story_dog.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 3f, 0);
        yield return new WaitForSeconds(1f);
        story_cat.SetActive(true);
        story_cat.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 3f, 0);
        Debug.Log("coroutine4 end");
    }

    private IEnumerator Story5Coroutine()
    {
        if (!skip) {
            yield return new WaitForSeconds(2f);
        }
        hint.enabled = false;
        A.SetActive(false);
        story4.SetActive(false);
        story5.SetActive(true);
        Color tmp = Color.black;
        tmp.a = 0.7f;
        mask.GetComponent<SpriteRenderer>().color = tmp;
        Vector3 temp = text.rectTransform.anchoredPosition;
        temp.y = -450;
        text.rectTransform.anchoredPosition = temp;
        text.color = Color.white;
        ShowScript();
        yield return new WaitForSeconds(6.5f);
        scarecrow.SetActive(true);
        hint.text = "Press         to load map....";
        hint.enabled = true;
        A.SetActive(true);
        Debug.Log("coroutine5 end");
    }

}
