using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public TextMeshProUGUI countTime;
    
    private Slider _slider;
    
    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        // time up
        if (_slider.value <= 0)
        {
            _slider.value = 0;
            
            // destroy itself
            Destroy(gameObject);
        }
        
        // decrement time
        _slider.value -= Time.deltaTime;
        
        // modify text
        countTime.text = Mathf.Floor(_slider.value).ToString();
    }
    
    private void OnDestroy()
    {
        // TODO: upon destroy, send a signal to the GameController
        
    }
}
