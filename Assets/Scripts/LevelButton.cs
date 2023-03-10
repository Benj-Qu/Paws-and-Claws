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
    void Start()
    {
        messageText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SelectLevel()
    {
        // Debug.Log(messageText.text);
        // HasInventory.Reset_static();
        // SceneManager.LoadScene(messageText.text);
        SceneManager.LoadScene("gold_spike_level");
    }
    
    public void ExitGame()
    {
        SceneManager.LoadScene("Intro");
    }
    
    public void RestartGame()
    {
        // HasInventory.Reset_static();
        SceneManager.LoadScene(GameObject.Find("GameController").GetComponent<GameController>().GetLevelName());
    }

    public void Cancel()
    {
        panel.SetActive(false);
    }
}
