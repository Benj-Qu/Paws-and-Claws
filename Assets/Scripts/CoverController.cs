using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CoverController : MonoBehaviour
{
    public GameObject loadingManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("A1") || Input.GetButtonDown("A2"))
        {
            loadingManager.GetComponent<Michsky.LSS.LoadingScreenManager>().LoadScene("Story");
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("click");
        //    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    var hit = Physics2D.Raycast(new Vector2(ray.origin.x, ray.origin.y), Vector2.zero, Mathf.Infinity);
        //    if (hit.transform != null)
        //    {
        //        //SceneManager.LoadScene("Story");
        //    }
        //}
    }
}

