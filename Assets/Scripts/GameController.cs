
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    // added by zeyi
    public int round_big = 1;
    /*temporarily added by zeyi*/
    // private bool a = false;
    /*temporarily added by zeyi*/

    public blockController bc;
    public GameObject Grid;
    public static GameController instance;
    public GameObject explosionAes;
    public string level;
    private TextMeshProUGUI winText;
    private GameObject player1;
    private GameObject player2;
    private Camera camera_;
    public GameObject selectionPanel;

    private Vector3 StartPoint1;
    private Vector3 StartPoint2;

    private flagController flagController;
    
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
        // Screen.SetResolution(960, 720, false);
        level = SceneManager.GetActiveScene().name;
        // scene = SceneManager.GetActiveScene().name[7] - '0';
        // scene = 0;
        Debug.Log("Level: "+ level);
        StartPoint1 = GameObject.Find("StartPoint").transform.position;
        StartPoint2 = StartPoint1;
        StartPoint1.x -= 0.4f;
        StartPoint2.x += 0.4f;
        if (level == "Tutorial")
        {
            StartPoint1.x -= 5f;
            StartPoint2.x += 5f;
        }
        if (level == "Tutorial_Player_Control")
        {
            StartPoint1 = GameObject.Find("StartPoint").transform.position;
            StartPoint2 = GameObject.Find("StartPoint2").transform.position;
        } else
        {
            flagController = GameObject.Find("Flags").GetComponent<flagController>();
        }
        player1 = GameObject.Find("player_1");
        player2 = GameObject.Find("player_2");
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
            ExitMenu.SetActive(true);
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
            winText.text = "Michigan Wins!";
        }
        else if (player1.GetComponent<PlayerScore>().getScore() < player2.GetComponent<PlayerScore>().getScore())
        {
            winText.text = "Ohio Wins!";
        }
        else
        {
            winText.text = "Tie! Try again!";
        }
        
        yield return new WaitForSeconds(2);
        // SceneManager.LoadScene("Intro");
        // if (level != 6)
        // {
        //     SceneManager.LoadScene("Level" + (level + 1));
        // }
        // else
        // {
            SceneManager.LoadScene("Intro");
        // }
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
        if (level != "Tutorial")
        {
            progressBar.gameObject.SetActive(true);
            progressBar.StartGame();
        }
        stage ++;
        Debug.Log("stage: " + stage);
        // TODO: set player movement true
        if (stage == 2) // start fight
        {
            player1.GetComponent<PlayerController>().activate();
            player2.GetComponent<PlayerController>().activate();
            selectionPanel.GetComponent<Selection>().DoneWithPlacement();
            Grid.SetActive(false);
            bc.RemoveBox();
        }
        else if (stage == 1) // start place block
        {
            if (flagController) flagController.FlagGeneration();
        }
        else // stage == 3 means the previous round is over
        {
            round_big++;
            stage = 0;
            EventBus.Publish<BigRoundIncEvent>(new BigRoundIncEvent(round_big));
            progressBar.gameObject.SetActive(false);
            // Reset the players
            player1.GetComponent<PlayerController>().deactivate();
            player2.GetComponent<PlayerController>().deactivate();
            player1.transform.position = StartPoint1;
            player2.transform.position = StartPoint2;
            // Disable the flags and clear the color
            if (flagController) flagController.DestroyFlags();
            player1.GetComponent<PlayerScore>().resetFlag();
            player2.GetComponent<PlayerScore>().resetFlag();
            // Show animation of next round
            // StartCoroutine()
        }
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
        return "level" + level;
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
