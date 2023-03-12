
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject explosionAes;
    private int level = 0;
    private TextMeshProUGUI winText;
    private GameObject player1;
    private GameObject player2;
    private Camera camera_;

    private Vector3 StartPoint1;
    private Vector3 StartPoint2;
    
    private int score = 0;
    
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
        level = SceneManager.GetActiveScene().name[5] - '0';
        // scene = SceneManager.GetActiveScene().name[7] - '0';
        // scene = 0;
        Debug.Log("Level: "+ level);

        StartPoint1 = GameObject.Find("StartPoint").transform.position;
        StartPoint2 = StartPoint1;
        StartPoint1.x -= 0.5f;
        StartPoint2.x += 0.5f;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitMenu.SetActive(true);
        }
    }

    public void GameWin()
    {
        // TODO: Add coroutine for animation
        Debug.Log("Win!");
        StartCoroutine(Win());
    }

    private IEnumerator Win()
    {
        if (score > 0)
        {
            winText.text = "Michigan Wins!";
        }
        else if (score < 0)
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
        progressBar.gameObject.SetActive(true);
        progressBar.StartGame();
        stage ++;
        // TODO: set player movement true
        if (stage == 2)
        {
            player1.GetComponent<PlayerController>().activate();
            player2.GetComponent<PlayerController>().activate();
            GameObject.Find("SelectionPanel").GetComponent<Selection>().DoneWithPlacement();
            GameObject.Find("Flags").GetComponent<flagController>().FlagGeneration();
        }
    }

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
    
    public void ChangeScore(int delta)
    {
        score += delta;
    }
}
