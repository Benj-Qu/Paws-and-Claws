using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelButton : MonoBehaviour
{
    private TextMeshProUGUI messageText;
    public GameObject panel;
    public GameObject loadingManager;
    void Start()
    {
        messageText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SelectLevel()
    {
        // Debug.Log(messageText.text);
        // HasInventory.Reset_static();
        //SceneManager.LoadScene(messageText.text);
        loadingManager.GetComponent<Michsky.LSS.LoadingScreenManager>().LoadScene(messageText.text);
    }
    
    public void ExitGame()
    {
        // SceneManager.LoadScene("NewIntro");
        loadingManager.GetComponent<Michsky.LSS.LoadingScreenManager>().LoadScene("NewIntro");
        GameController.instance.pause = false;
        Time.timeScale = 1f;
    }
    
    public void RestartGame()
    {
        // HasInventory.Reset_static();
        EventBus.Publish<LoadSceneEvent>(new LoadSceneEvent());
        SceneManager.LoadScene(GameController.instance.level);
        GameController.instance.pause = false;
        Time.timeScale = 1f;
    }

    public void Cancel()
    {
        panel.SetActive(false);
        GameController.instance.pause = false;
        Time.timeScale = 1f;
    }
}
