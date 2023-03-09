using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public GameObject Choice1;
    public GameObject Choice2;

    private CardSelection _cardSelection1;
    private CardSelection _cardSelection2;

    private bool _done = false;

    // Start is called before the first frame update
    void Start()
    {
        _cardSelection1 = Choice1.GetComponent<CardSelection>();
        _cardSelection2 = Choice2.GetComponent<CardSelection>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_done && _cardSelection1.roundUp && _cardSelection2.roundUp)
        {
            _done = true;
            // TODO: send gameController that the battle can begins.
            Debug.Log("battle begin");
        }
    }
}
