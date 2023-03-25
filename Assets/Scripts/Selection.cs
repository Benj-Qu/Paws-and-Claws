using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public GameObject Choice1;
    public GameObject Choice2;

    private CardSelection _cardSelection1;
    private CardSelection _cardSelection2;
    private GameController gameController;
    private blockController BlockController;

    public bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        _cardSelection1 = Choice1.GetComponent<CardSelection>();
        _cardSelection2 = Choice2.GetComponent<CardSelection>();
        GameObject temp = GameObject.Find("GameController");
        if (temp) gameController = temp.GetComponent<GameController>();
        GameObject temp1 = GameObject.Find("Block");
        if (temp1) BlockController = temp1.GetComponent<blockController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!done && _cardSelection1.roundUp && _cardSelection2.roundUp)
        {
            done = true;
            // send gameController that the placement can begins,
            StartCoroutine(DelayStartGame());
        }
    }

    private IEnumerator DelayStartGame()
    {
        // delay 0.5 second for effect to take place
        yield return new WaitForSeconds(0.5f);
        if (gameController)
        {
            // placement begin
            Debug.Log("selection call");
            gameController.StartGame();
        }
    }

    // call this when the placement is done
    public void DoneWithPlacement()
    {
        // set itself as inactive and destroy these blocks that are not put down on map
        if (BlockController)
        {
            foreach (blockMovement block in BlockController.bm)
            {
                if (block.set == false)
                {
                    Destroy(block.gameObject);
                }
            }
        } 
        // gameObject.SetActive(false);
        Destroy(gameObject);
        // upon the next round, reinitialize 
    }
    
}
