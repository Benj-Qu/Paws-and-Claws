
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    private int level = 0;
    private TextMeshProUGUI winText;
    private GameObject player1;
    private GameObject player2;
    private Camera camera_;

    private GameObject StartPoint;
    // public Vector3[] StartPoint;
    public GameObject ExitMenu;
    // Start is called before the first frame update
    
    void Start()
    {
        // Screen.SetResolution(960, 720, false);
        level = SceneManager.GetActiveScene().name[5] - '0';
        // scene = SceneManager.GetActiveScene().name[7] - '0';
        // scene = 0;
        Debug.Log("Level: "+ level);

        StartPoint = GameObject.Find("StartPoint");
        player1 = GameObject.Find("player_1");
        player2 = GameObject.Find("player_2");
        player1.transform.position = StartPoint.transform.position;
        player2.transform.position = StartPoint.transform.position;
        camera_ = Camera.main;
        winText = GameObject.Find("Win Text").GetComponent<TextMeshProUGUI>();
        ExitMenu.SetActive(false);
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
        winText.text = "You Win!";
        yield return new WaitForSeconds(2);
        // SceneManager.LoadScene("Intro");
        if (level != 6)
        {
            SceneManager.LoadScene("Level" + (level + 1));
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
        StartCoroutine(Lose());
    }

    public void Killed(GameObject player)
    {
        
    }

    private IEnumerator Lose()
    {
        winText.text = "Try again!";
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Level" + level);
        // player.GetComponent<HasInventory>().Reset();
    }

    public string GetLevelName()
    {
        return "level" + level;
    }
}
