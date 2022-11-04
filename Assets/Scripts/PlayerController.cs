using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
internal class PlayerController : MonoBehaviour
{

    private Player player;
    private PlayerMotor motor;
    private PlayerInputs _inputs;
    private TimeSwitch timeSwitch;

    [SerializeField]
    private float mouseSensX, mouseSensY;
    [SerializeField]
    private float cameraPitch;



    [SerializeField]
    private bool cursorLock = true;

    private bool jumpQueued = false;

    private void UpdateCursorLock(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }


    public PlayerInputs Inputs
    {
        get { return _inputs; }
    }

    void Awake()
    {
        _inputs = new PlayerInputs();
        motor = GetComponent<PlayerMotor>();
        player = GetComponent<Player>();
        timeSwitch = GetComponent<TimeSwitch>();
        UpdateCursorLock(cursorLock);
    }

    private void Update()
    {
        UpdateInputs();
        Look();
    }

    private void FixedUpdate()
    {
        jumpQueued = false;
    }

    private void Look()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Camera.main.transform.localEulerAngles = Vector3.right * (cameraPitch = Mathf.Clamp(cameraPitch - mouseDelta.y * mouseSensY, -90, 90));
        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensX);
    }

    private void UpdateInputs()
    {
        Vector3 movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        bool sprinting = Input.GetKey(KeyCode.LeftShift);
        if (Input.GetButtonDown("Jump"))
        {
            jumpQueued = true;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            player.AttemptInteract();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            timeSwitch.SwitchTime();
        }
        _inputs.Update(movementInput, sprinting, jumpQueued);
    }

    public class PlayerInputs
    {
        public Vector3 movement;
        public bool sprinting;
        public bool jumping;

        public PlayerInputs()
        {
            movement = Vector3.zero;
            sprinting = false;
            jumping = false;
        }

        // Ensure all fields are updated!
        public void Update(Vector3 _movement, bool _sprinting, bool _jumping)
        {
            movement = _movement;
            sprinting = _sprinting;
            jumping = _jumping;
        }
    }

}