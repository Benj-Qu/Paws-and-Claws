using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowAddScore : MonoBehaviour
{
    public GameObject flag;
    public GameObject textObject;
    private RectTransform textTransform;
    // Start is called before the first frame update
    void Start()
    {
        textTransform = textObject.GetComponent<RectTransform>();
    }
    
    // // Update is called once per frame
    // void Update()
    // {
    //     
    // }

    public void ShowScore()
    {
        textObject.SetActive(true);
        StartCoroutine(ShowScoreAnimation());
    }

    private IEnumerator ShowScoreAnimation()
    {
        Vector2 position;
        position.x = (flag.transform.position.x + 1) * 50;
        position.y = flag.transform.position.y * 40;
        textTransform.anchoredPosition = position;
        // textObject.transform.position = position;
        yield return new WaitForSeconds(0.2f);
        textObject.SetActive(false);
    }
}