using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int joystickNumber;
    public KeyCode FireButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        string joystickString = joystickNumber.ToString();
        if ((Input.GetAxis("Fire" + joystickString) != 0) || Input.GetKey(FireButton))
        {
            Debug.Log("Fire" + joystickString);
            // TODO
        }
    }
}
