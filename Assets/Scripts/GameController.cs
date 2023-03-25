using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // added by zeyi
    public int round_big = 1;

    public blockController bc;
    public GameObject Grid;
    public static GameController instance;
    public GameObject explosionAes;
    public string level;
    public float dist = 0.4f;
    public GameObject WinImage;
    public TextMeshProUGUI ScoreText;
    
    private TextMeshProUGUI winText;
    private GameObject player1;
    private GameObject player2;
    private PlayerController player1_control;
    private PlayerController player2_control;
    private Camera camera_;
    private int score1Big = 0;
    private int score2Big = 0;
    public GameObject selectionPanel;
    public GameObject mask;
    public GameObject follower;

    private Vector3 StartPoint1;
    private Vector3 StartPoint2;

    public bool pause = false;

    private flagController flagController;
    public ScoreDisplayer sd;
    
    // public Vector3[] StartPoint;
    public GameObject ExitMenu;
    public int stage = 0;

    public ProgressBar_Main progressBar;
    // Start is called before the first frame update

    private void Awake()
    {
        Destroy(instance);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            // destroy game object this script attached to, not the script itself. In this situation, the gameController
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Screen.SetResolution(1600, 1000, false);
        level = SceneManager.GetActiveScene().name;
        // scene = SceneManager.GetActiveScene().name[7] - '0';
        // scene = 0;
        Debug.Log("Level: "+ level);
        StartPoint1 = GameObject.Find("StartPoint").transform.position;
        StartPoint2 = StartPoint1;
        StartPoint1.x -= dist;
        StartPoint2.x += dist;
        // if (level == "Tutorial")
        // {
        //     StartPoint1.x -= 5f;
        //     StartPoint2.x += 5f;
        // }
        if (level == "Tutorial")
        {
            StartPoint1 = GameObject.Find("StartPoint").transform.position;
            StartPoint2 = GameObject.Find("StartPoint2").transform.position;
        } else
        {
            flagController = GameObject.Find("Flags").GetComponent<flagController>();
        }
        player1 = GameObject.Find("player_1");
        player2 = GameObject.Find("player_2");
        player1_control = player1.GetComponent<PlayerController>();
        player2_control = player2.GetComponent<PlayerController>();
        player1.transform.position = StartPoint1;
        player2.transform.position = StartPoint2;
        camera_ = Camera.main;
        winText = GameObject.Find("Win Text").GetComponent<TextMeshProUGUI>();
        ExitMenu.SetActive(false);

        // added by zeyi
        explosionAes = Resources.Load<GameObject>("Prefab/Explosion");
        progressBar.gameObject.SetActive(false);
        // call this with the local attribute round_big when the round increment
        EventBus.Publish<BigRoundIncEvent>(new BigRoundIncEvent(round_big));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pause)
            {
                ExitMenu.SetActive(true);
                Time.timeScale = 0f;
                pause = true;
            }
            else
            {
                ExitMenu.SetActive(false);
                Time.timeScale = 1f;
                pause = false;
            }
        }

        if (stage != 2)
        {
            if (player1_control.isActive())
            {
                player1_control.deactivate();
            }
            if (player2_control.isActive())
            {
                player2_control.deactivate();
            }
        }

        /*temporarily added by zeyi*/
        // if (!a && stage == 2)
        // {
        //     round_big++;
        //     a = true;
        //     EventBus.Publish<BigRoundIncEvent>(new BigRoundIncEvent(round_big));
        // }
        /*temporarily added by zeyi*/
    }

    public void GameWin()
    {
        // TODO: Add coroutine for animation
        Debug.Log("Win!");
        player1.GetComponent<PlayerScore>().resetFlag();
        player2.GetComponent<PlayerScore>().resetFlag();
        StartCoroutine(Win());
    }

    private IEnumerator Win()
    {
        if (player1.GetComponent<PlayerScore>().getScore() > player2.GetComponent<PlayerScore>().getScore())
        {
            winText.text = "DOG Wins!";
        }
        else if (player1.GetComponent<PlayerScore>().getScore() < player2.GetComponent<PlayerScore>().getScore())
        {
            winText.text = "CAT Wins!";
        }
        else
        {
            winText.text = "Tie! Try again!";
        }
        
        yield return new WaitForSeconds(2);
        // SceneManager.LoadScene("Intro");
        if (level == "Tutorial")
        {
            SceneManager.LoadScene("Trial Level");
        }
        else
        {
            SceneManager.LoadScene("Intro");
        }
        // player.GetComponent<HasInventory>().Reset();
    }

    public void GameOver()
    {
        // TODO: Add coroutine for animation
        // SceneManager.LoadScene("Level" + level);
        StartCoroutine(Win());
    }

    public void Killed(GameObject player)
    {
        // TODO: Cool down time? how?
        if (player.name == "player_1")
        {
            player.transform.position = StartPoint1;
        }
        else
        {
            player.transform.position = StartPoint2;
        }
    }

    public void StartGame()
    {
        // Start time countdown
        if (level != "Tutorial1")
        {
            progressBar.gameObject.SetActive(true);
            progressBar.StartGame();
        }
        stage ++;
        Debug.Log("stage: " + stage);
        // TODO: set player movement true
        if (stage == 2) // start fight
        {
            if (level == "Tutorial1")
            {
                StartCoroutine(FinishTutorial());
            }

            if (mask)
            {
                Color tmp = mask.GetComponent<SpriteRenderer>().color;
                tmp.a = 0.14f;
                mask.GetComponent<SpriteRenderer>().color = tmp;
            }
            
            player1.GetComponent<PlayerController>().activate();
            player2.GetComponent<PlayerController>().activate();
            selectionPanel.GetComponent<Selection>().DoneWithPlacement();
            Grid.SetActive(false);
            bc.RemoveBox();
            follower.SetActive(false);
        }
        else if (stage == 1) // start place block
        {
            if (flagController) flagController.FlagGeneration();
            follower.SetActive(true);
        }
        else // stage == 3 means the previous round is over
        {
            StartCoroutine(ShowScore());
        }
    }

    private IEnumerator ShowScore()
    {
        round_big++;
        stage = 0;
        int score = sd.GetWinner();
        if (score == 1)
        {
            score1Big += 1;
        } else if (score == -1)
        {
            score2Big += 1;
        }
        else
        {
            score1Big += 1;
            score2Big += 1;
        }

        ScoreText.text = score1Big.ToString() + " : " + score2Big.ToString();
        if (score1Big > score2Big)
        {
            WinImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/DogWin");
        }
        else if (score1Big < score2Big)
        {
            WinImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/CatWin");
        }
        else
        {
            WinImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/VSS");
        }
        WinImage.SetActive(true);
        yield return new WaitForSeconds(2f);
        WinImage.SetActive(false);
        EventBus.Publish<BigRoundIncEvent>(new BigRoundIncEvent(round_big));
        progressBar.gameObject.SetActive(false);
        // Reset the players
        player1.GetComponent<PlayerController>().reset();
        player2.GetComponent<PlayerController>().reset();
        player1.GetComponent<PlayerController>().deactivate();
        player2.GetComponent<PlayerController>().deactivate();
        player1.transform.position = StartPoint1;
        player2.transform.position = StartPoint2;
        // Disable the flags and clear the color
        if (flagController) flagController.DestroyFlags();
        player1.GetComponent<PlayerScore>().resetFlag();
        player2.GetComponent<PlayerScore>().resetFlag();
        sd.Reset();
        // Show animation of next round
        // StartCoroutine()
        if (mask)
        {
            Color tmp = mask.GetComponent<SpriteRenderer>().color;
            tmp.a = 0.27f;
            mask.GetComponent<SpriteRenderer>().color = tmp;
        }
        Grid.SetActive(true);
    }
    
    private IEnumerator FinishTutorial()
    {
        winText.text = "You got it!";
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Trial Level");
        winText.text = "";
    }
    
    // private IEnumeratorr 

    private IEnumerator Lose()
    {
        winText.text = "Try again!";
        yield return new WaitForSeconds(1);
        // SceneManager.LoadScene("Level" + level);
        SceneManager.LoadScene("gold_spike_level");
        // player.GetComponent<HasInventory>().Reset();
    }

    public string GetLevelName()
    {
        return level;
    }
}

public class BigRoundIncEvent
{
    public int round_big;
    public BigRoundIncEvent(int _round_big)
    {
        round_big = _round_big;
    }

    public override string ToString()
    {
        return "Change to big round " + round_big;
    }
}
