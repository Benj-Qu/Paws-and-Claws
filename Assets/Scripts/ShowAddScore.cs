using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowAddScore : MonoBehaviour
{
    public GameObject flag;
    public TextMeshProUGUI textObject;
    private RectTransform textTransform;
    // Start is called before the first frame update
    void Start()
    {
        textObject.enabled = false;
        textTransform = textObject.GetComponent<RectTransform>();
    }
    
    // // Update is called once per frame
    // void Update()
    // {
    //     
    // }

    public void ShowScore()
    {
        textObject.enabled = true;
        StartCoroutine(ShowScoreAnimation());
    }

    private IEnumerator ShowScoreAnimation()
    {
        Vector2 position;
        position.x = (flag.transform.position.x + 2) * 40;
        position.y = flag.transform.position.y * 30;
        textTransform.anchoredPosition = position;
        // textObject.transform.position = position;
        yield return new WaitForSeconds(0.2f);
        textObject.enabled = false;
    }
}
