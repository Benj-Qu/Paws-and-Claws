using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelButton : MonoBehaviour
{
    private TextMeshProUGUI messageText;
    private GameController gameController;
    public GameObject panel;
    void Start()
    {
        messageText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void SelectLevel()
    {
        // Debug.Log(messageText.text);
        // HasInventory.Reset_static();
        SceneManager.LoadScene(messageText.text);
    }
    
    public void ExitGame()
    {
        SceneManager.LoadScene("Intro");
    }
    
    public void RestartGame()
    {
        // HasInventory.Reset_static();
        SceneManager.LoadScene(gameController.level);
    }

    public void Cancel()
    {
        panel.SetActive(false);
    }
}
