using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinRecord : MonoBehaviour
{
    public static WinRecord Instance;

    public enum Status {Tie, Cat, Dog};

    public GameObject[] CatFlags;
    public GameObject[] DogFlags;

    private Status[] statuses = { Status.Tie, Status.Tie, Status.Tie, Status.Tie, Status.Tie };
    private Dictionary<string, int> scenes = new Dictionary<string, int> () {
        {"Farm", 0}, {"Winter Land", 1}, {"Lantern Festival", 2}, {"Iceland", 3}, {"Volcano", 4}
    };
    private string current = "NewIntro";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // called first
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        current = scene.name;
        DeActiveFlags();
        if (current == "NewIntro")
        {
            for (int i = 0; i < 5; i++)
            {
                if (statuses[i] == Status.Cat)
                {
                    CatFlags[i].SetActive(true);
                }
                else if (statuses[i] == Status.Dog)
                {
                    DogFlags[i].SetActive(true);
                }
            }
        }
    }

    // called when the game is terminated
    private void OnDisable()
    {
        DeActiveFlags();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void DeActiveFlags()
    {
        foreach (GameObject flag in CatFlags)
        {
            flag.SetActive(false);
        }
        foreach (GameObject flag in DogFlags)
        {
            flag.SetActive(false);
        }
    }

    public void UpdateWinning(Status winner)
    {
        if (winner != Status.Tie)
        {
            statuses[scenes[current]] = winner;
        }
    }

    public void reset()
    {
        statuses = new Status[] { Status.Tie, Status.Tie, Status.Tie, Status.Tie, Status.Tie };
    }
}
