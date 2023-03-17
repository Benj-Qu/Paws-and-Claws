using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFlag : MonoBehaviour
{
    public MovementTutorial mt;
    public bool touched = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && touched == false)
        {
            mt.target += 1;
            touched = true;
        }
    }
}
