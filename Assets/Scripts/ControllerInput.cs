using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerInput : MonoBehaviour
{
    // private PlayerXboxControls controls;
    private InputAction Jump;
    public int joystickNumber;
    private Vector2 moveValue;
    private PlayerController pc;
    public float Speed = 3;

    private void Awake() {
        // controls = new PlayerXboxControls();
        // Jump = new InputAction("A" + joystickNumber, InputActionType.Button, "<Gamepad>/buttonSouth");
        // Jump.Enable();
        // Jump.performed += ctx => XboxCheckAndJump();
        pc = GetComponent<PlayerController>();
        // controls.Gameplay.Move.performed += ctx => XboxCheckAndJump();
    }
    
    // private void OnEnable() {
    //     controls.Gameplay.Enable();
    // }
    //
    // private void OnDisable() {
    //     controls.Gameplay.Disable();
    // }
    
    public void XboxCheckAndJump(InputAction.CallbackContext context) {
        if (pc.jumpable())
        {
            pc.jump();
        }
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        moveValue = context.ReadValue<Vector2>() * Time.deltaTime * Speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
