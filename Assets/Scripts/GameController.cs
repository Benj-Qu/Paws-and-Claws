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

    public GameObject loadingManager;

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
    public GameObject blockSelectionMask;
    public GameObject follower;
    public GameObject IceInvalidPositions;

    public GameObject FarmStage1Objects;
    public GameObject FarmStage1Texts;
    public GameObject FarmStage2Objects;
    public GameObject FarmStage2Texts;
    public GameObject FarmStage3Objects;
    public GameObject FarmStage3Texts;
    public TextMeshProUGUI FarmWinText;
    public bool attackTutorialFinished = false;

    private Vector3 StartPoint1;
    private Vector3 StartPoint2;

    public bool pause = false;

    private flagController flagController;
    public int flags = 0;
    
    // public Vector3[] StartPoint;
    public GameObject ExitMenu;
    public int stage = 0;

    public int guardian_speaking = 0; //0: speaking, 1: finish speaking, 2: finish speaking & player pressed A
    public GameObject GuardianSpeaking;
    public GameObject GuardianMask;
    public textController FarmStoryText;
    private int stage0_cnt = 0;
    public int finished_stage0_cnt = -1;

    // public ProgressBar_Main progressBar;
    public TimeDisplayer progressBar;
    
    public GameObject ScorePanel;

    private TextMeshProUGUI RoundTextHint;

    private WinRecord winRecord;
    Subscription<LoadSceneEvent> LoadSceneEvent_subscription;
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
        LoadSceneEvent_subscription = EventBus.Subscribe<LoadSceneEvent>(WaitAndStart);
    }

    void Start()
    {
        //Screen.SetResolution(1600, 1000, false);
        level = SceneManager.GetActiveScene().name;
        // scene = SceneManager.GetActiveScene().name[7] - '0';
        // scene = 0;
        Debug.Log("Level: "+ level);
        StartPoint1 = GameObject.Find("StartPoint").transform.position;
        GameObject wrgb = GameObject.Find("WinRecord");
        if (wrgb)
        {
            winRecord = wrgb.GetComponent<WinRecord>();
        }
        StartPoint2 = StartPoint1;
        StartPoint1.x -= dist;
        StartPoint2.x += dist;
        player1 = GameObject.Find("player_1");
        player2 = GameObject.Find("player_2");
        player1_control = player1.GetComponent<PlayerController>();
        player2_control = player2.GetComponent<PlayerController>();
        if (level == "Farm")
        {
            progressBar.gameObject.SetActive(false);
            StartPoint1 = GameObject.Find("StartPoint").transform.position;
            StartPoint2 = GameObject.Find("StartPoint2").transform.position;
            stage = -2; // -2: movement, -1: attack
            FarmStage1Objects.SetActive(true);
            // FarmStage1Texts.SetActive(true);
            player1_control.activate();
            player2_control.activate();
        } 
        flagController = GameObject.Find("Flags").GetComponent<flagController>();
        player1.transform.position = StartPoint1;
        player2.transform.position = StartPoint2;
        camera_ = Camera.main;
        if(level == "Farm")
        {
            winText = FarmWinText;
        }
        else
        {
            winText = GameObject.Find("Win Text").GetComponent<TextMeshProUGUI>();
        }
        ExitMenu.SetActive(false);

        // added by zeyi
        explosionAes = Resources.Load<GameObject>("Prefab/Explosion");
        // progressBar.gameObject.SetActive(false);
        // call this with the local attribute round_big when the round increment
        RoundTextHint = GameObject.Find("RoundTextHint").GetComponent<TextMeshProUGUI>();
    }

    private void WaitAndStart (LoadSceneEvent e)
    {
        Debug.Log("Wait");
        if(level != "Farm")
        {
            EventBus.Publish<BigRoundIncEvent>(new BigRoundIncEvent(round_big));
            ScorePanel = GameObject.Find("ScorePanel");
            if (ScorePanel) ScorePanel.SetActive(false);
            if (WinImage && WinImage.activeSelf == false) WinImage.SetActive(true);
        }
        else
        {
            StartCoroutine(GuardianCoroutine());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Todo: delete record flags
            SceneManager.LoadScene("Cover");
            winRecord.reset();
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("X1") || Input.GetButtonDown("X2"))
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

        if (level != "Farm" && stage != 2)
        {
            if (player1_control.isActive())
            {
                player1_control.deactivate();
                player1_control.resetAnim();
            }
            if (player2_control.isActive())
            {
                player2_control.deactivate();
                player2_control.resetAnim();
            }
        }
        
        if(level == "Farm")
        {
            if (guardian_speaking == 0)
            {
                // if guardian is speaking, deactivate playerss
                player1_control.deactivate();
                player2_control.deactivate();
                player1_control.resetAnim();
                player2_control.resetAnim();
            }
            if(guardian_speaking == 1 && (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("A1") || Input.GetButtonDown("A2")))
            {
                // if guardian finishes speaking and player pressed A, activate players
                if(stage == -2 && stage0_cnt < 2)
                {
                    if(finished_stage0_cnt == stage0_cnt)
                    {
                        if (stage0_cnt == 0) FarmStoryText.updateText("[speed=0.08]<b>The rule is simple:\n Seize the FLAGS and outscore your opponent!</b>");
                        if (stage0_cnt == 1) FarmStoryText.updateText("[speed=0.08]<b>First skill you should master:\n WALL JUMPING!</b>");
                        stage0_cnt++;
                    }
                }
                else
                {
                    if (stage == -2 && finished_stage0_cnt < stage0_cnt) return;
                    guardian_speaking = 2;
                    GuardianMask.SetActive(false);
                    player1_control.activate();
                    player2_control.activate();
                }
            }
            if(stage == -2 && flags == 2)
            {
                // if two flags are all obtained by players, start attack tutorial
                StartCoroutine(finishStageT1());
            }
            if(stage == -1 && attackTutorialFinished == true)
            {
                // if players successfully attacked each other, start game tutorial
                StartCoroutine(finishStageT2());
            }
            if(stage != -2 && stage != -1 && stage != 2)
            {
                // deactivate players during block selection and placement
                if (player1_control.isActive())
                {
                    player1_control.deactivate();
                    player1_control.resetAnim();
                }
                if (player2_control.isActive())
                {
                    player2_control.deactivate();
                    player2_control.resetAnim();
                }
            }
        }
    }

    IEnumerator GuardianCoroutine()
    {
        yield return new WaitForSeconds(3f);
        Guardian_Speak();
    }

    void Guardian_Speak()
    {
        guardian_speaking = 0;
        GuardianSpeaking.SetActive(true);
        GuardianMask.SetActive(true);
    }
    
    IEnumerator finishStageT1()
    {
        stage = -1; // attack tutorial
        yield return new WaitForSeconds(2f);
        Guardian_Speak();
        FarmStage1Objects.SetActive(false);
        FarmStage1Texts.SetActive(false);
        FarmStage2Objects.SetActive(true);
        // FarmStage2Texts.SetActive(true);
        ResetPlayers();
    }

    IEnumerator finishStageT2()
    {
        stage = 0; // start game
        yield return null;
        Guardian_Speak();
        FarmStage2Objects.SetActive(false);
        FarmStage2Texts.SetActive(false);
        FarmStage3Objects.SetActive(true);
        // FarmStage3Texts.SetActive(true);
        ResetPlayers();
    }

    public void tutorialCall()
    {
        EventBus.Publish<BigRoundIncEvent>(new BigRoundIncEvent(round_big));
        if (WinImage && WinImage.activeSelf == false) WinImage.SetActive(true);
    }


    public void ResetPlayers()
    {
        player1.transform.position = GameObject.Find("StartPoint").transform.position;
        player2.transform.position = GameObject.Find("StartPoint2").transform.position;
        player1_control.resetAnim();
        player2_control.resetAnim();
    }

    private IEnumerator Win()
    {
        RoundTextHint.enabled = false;
        if (flagController) flagController.DestroyFlags();
        ScorePanel.SetActive(false);
        if(FarmStoryText) FarmStoryText.updateText("");
        // if (player1.GetComponent<PlayerScore>().getScore() > player2.GetComponent<PlayerScore>().getScore())
        // {
        //     if (score1Big > score2Big)
        //     {
        //         // WinImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/DogWin");
        //         WinImage.GetComponent<WinImage>().DogWin();
        //         ScoreText.text = "DOG Wins!";
        //     }
        //     else if (score1Big < score2Big)
        //     {
        //         // WinImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/CatWin");
        //         WinImage.GetComponent<WinImage>().CatWin();
        //         ScoreText.text = "CAT Wins!";
        //     }
        //     else
        //     {
        //         // WinImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/VSS");
        //         WinImage.GetComponent<WinImage>().VS();
        //         ScoreText.text = "Tie!";
        //     }
        //     WinImage.SetActive(true);
        //     // winText.text = "DOG Wins!";
        // }
        // else if (player1.GetComponent<PlayerScore>().getScore() < player2.GetComponent<PlayerScore>().getScore())
        // {
        //     
        //     if (score1Big > score2Big)
        //     {
        //         // WinImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/DogWin");
        //         WinImage.GetComponent<WinImage>().DogWin();
        //         ScoreText.text = "DOG Wins!";
        //     }
        //     else if (score1Big < score2Big)
        //     {
        //         // WinImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/CatWin");
        //         WinImage.GetComponent<WinImage>().CatWin();
        //         ScoreText.text = "CAT Wins!";
        //     }
        //     else
        //     {
        //         // WinImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/VSS");
        //         WinImage.GetComponent<WinImage>().VS();
        //         ScoreText.text = "Tie!";
        //     }
        //     WinImage.SetActive(true);
        //     // winText.text = "CAT Wins!";
        // }
        // else
        // {
            if (score1Big > score2Big)
            {
                // WinImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/DogWin");
                WinImage.GetComponent<WinImage>().DogWin();
                ScoreText.text = "DOG Wins!";
                if (winRecord) 
                {
                    winRecord.UpdateWinning(WinRecord.Status.Dog);
                }
            }
            else if (score1Big < score2Big)
            {
                // WinImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/CatWin");
                WinImage.GetComponent<WinImage>().CatWin();
                ScoreText.text = "CAT Wins!";
                if (winRecord)
                {
                    winRecord.UpdateWinning(WinRecord.Status.Cat);
                }
            }
            else
            {
                // WinImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/VSS");
                WinImage.GetComponent<WinImage>().VS();
                ScoreText.text = "Tie!";
                if (winRecord)
                {
                    winRecord.UpdateWinning(WinRecord.Status.Tie);
                }
            }
            WinImage.SetActive(true);
            // winText.text = "Tie! Try again!";
        // }
        
        yield return new WaitForSeconds(2);
        //SceneManager.LoadScene("NewIntro");
        loadingManager.GetComponent<Michsky.LSS.LoadingScreenManager>().LoadScene("NewIntro");
        player1.GetComponent<PlayerController>().deactivate();
        player2.GetComponent<PlayerController>().deactivate();
        yield return new WaitForSeconds(6);
        player1.GetComponent<PlayerController>().activate();
        player2.GetComponent<PlayerController>().activate();
    }

    public void GameOver()
    {
        // TODO: Add coroutine for animation
        // SceneManager.LoadScene("Level" + level);
        Debug.Log("Level" + level);
        progressBar.gameObject.SetActive(false);
        // make the party time text disappear
        EventBus.Publish<GameOverEvent>(new GameOverEvent());
        int score = ScorePanel.GetComponent<ScorePanel>().GetWinner();
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
        // make tutorial1 finished

        progressBar.gameObject.SetActive(true);
        

        stage ++;
        Debug.Log("stage: " + stage);
        if (ScorePanel) ScorePanel.SetActive(false);
        // TODO: set player movement true
        if (stage == 2) // start fight
        {
            progressBar.StartFight();
            if (IceInvalidPositions)
            {
                IceInvalidPositions.SetActive(false);
            }

            // Remove Winter Land
            if (level == "Volcano")
            {
                VolcanoController vc = GameObject.Find("Volcano").GetComponent<VolcanoController>();
                vc.begin();
            }

            if (mask)
            {
                mask.GetComponent<SpriteRenderer>().color = Color.white;
                Color tmp = mask.GetComponent<SpriteRenderer>().color;
                tmp.a = 0.14f;
                mask.GetComponent<SpriteRenderer>().color = tmp;
            }
            
            player1.GetComponent<PlayerController>().activate();
            player2.GetComponent<PlayerController>().activate();
            selectionPanel.GetComponent<PlaySelection>().DoneWithPlacement();
            Grid.SetActive(false);
            bc.RemoveBox();
            follower.SetActive(false);
            if (level == "Farm")
            {
                FarmStage3Texts.SetActive(false);
                Guardian_Speak();
            }
            else
            {
                if (ScorePanel) ScorePanel.SetActive(true);
            }
        }
        else if (stage == 1) // start place block
        {
            if (IceInvalidPositions)
            {
                IceInvalidPositions.SetActive(true);
            }
            if (blockSelectionMask)
            {
                Color tmp = blockSelectionMask.GetComponent<SpriteRenderer>().color;
                tmp.a = 0f;
                blockSelectionMask.GetComponent<SpriteRenderer>().color = tmp;
            }
            mask.SetActive(true);
            progressBar.StartGame();
            flagController = GameObject.Find("Flags").GetComponent<flagController>();
            if (flagController) flagController.FlagGeneration();
            follower.SetActive(true);
            follower.transform.position = new Vector3(1000, 1000);
            if (mask)
            {
                mask.GetComponent<SpriteRenderer>().color = Color.white;
                Color tmp = mask.GetComponent<SpriteRenderer>().color;
                tmp.a = 0.27f;
                mask.GetComponent<SpriteRenderer>().color = tmp;
            }
            if (level == "Farm")
            {
                FarmStage3Texts.SetActive(false);
                Guardian_Speak();
            }
        }
        else // stage == 3 means the previous round is over
        {
            StartCoroutine(ShowScore());
        }
    }

    private IEnumerator ShowScore()
    {
        RoundTextHint.text = "";
        round_big++;
        stage = 0;
        int score = ScorePanel.GetComponent<ScorePanel>().GetWinner();
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

        if (score1Big - score2Big == 2 || score2Big - score1Big == 2)
        {
            GameOver();
        }
        else
        {
            ScoreText.text = score1Big.ToString() + " : " + score2Big.ToString();
            if (score1Big > score2Big)
            {
                // WinImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/DogWin");
                WinImage.GetComponent<WinImage>().DogWin();
            }
            else if (score1Big < score2Big)
            {
                // WinImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/CatWin");
                WinImage.GetComponent<WinImage>().CatWin();
            }
            else
            {
                // WinImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/VSS");
                WinImage.GetComponent<WinImage>().VS();
            }
            progressBar.gameObject.SetActive(false);
            WinImage.SetActive(true);
            yield return new WaitForSeconds(2f);
            RoundTextHint.text = "Round " + round_big + " / 3";
            WinImage.GetComponent<WinImage>().reset();
            WinImage.SetActive(false);
            EventBus.Publish<BigRoundIncEvent>(new BigRoundIncEvent(round_big));
            // Reset the players
            player1.GetComponent<PlayerController>().reset();
            player2.GetComponent<PlayerController>().reset();
            player1.GetComponent<PlayerController>().deactivate();
            player2.GetComponent<PlayerController>().deactivate();
            player1_control.resetAnim();
            player2_control.resetAnim();
            player1.transform.position = StartPoint1;
            player2.transform.position = StartPoint2;
            // Disable the flags and clear the color
            if (flagController) flagController.DestroyFlags();
            player1.GetComponent<PlayerScore>().resetFlag();
            player2.GetComponent<PlayerScore>().resetFlag();
            ScorePanel.GetComponent<ScorePanel>().reset();
            // Show animation of next round
            // StartCoroutine()
            if (mask)
            {
                mask.GetComponent<SpriteRenderer>().color = Color.white;
                Color tmp = mask.GetComponent<SpriteRenderer>().color;
                tmp.a = 0.27f;
                mask.GetComponent<SpriteRenderer>().color = tmp;
            }
            Grid.SetActive(true);
        }
    }
    
    //private IEnumerator FinishTutorial()
    //{
    //    winText.text = "You got it!";
    //    yield return new WaitForSeconds(2f);
    //    SceneManager.LoadScene("Trial Level");
    //    winText.text = "";
    //}
    
    //// private IEnumeratorr 

    //private IEnumerator Lose()
    //{
    //    winText.text = "Try again!";
    //    yield return new WaitForSeconds(1);
    //    // SceneManager.LoadScene("Level" + level);
    //    SceneManager.LoadScene("gold_spike_level");
    //    // player.GetComponent<HasInventory>().Reset();
    //}

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

public class GameOverEvent
{
    public GameOverEvent()
    {
        return;
    }
}
