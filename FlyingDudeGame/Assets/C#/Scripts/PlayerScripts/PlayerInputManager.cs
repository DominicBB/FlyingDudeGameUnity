using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private MouseClickManager mouseClickManager;
    private PlayerMovement playerMovement;

    private bool isFlying;

    private string xMovement = "Horizontal";
    private string yMovement = "Vertical";
    private KeyCode flyModeToggle = KeyCode.F;
    private string elevation = "Elevation";
    private KeyCode jumpKey = KeyCode.Space;

    private string fire1 = "Fire1";
    private string fire2 = "Fire2";
    private string fire3 = "Fire3";
    private string scrollWheel = "Mouse ScrollWheel";

    private KeyCode removeVelocity = KeyCode.C;
    private Vector3 stopVelocity = new Vector3(0, 0, 0);

    private float forwardBack;
    private float leftRight;
    private float elevationChange;
    private float scrollUD;

    private bool flyToggle;
    private bool stopMoving;
    private bool anyMouseButton;
    private bool jump;
    private bool[] mouseButtons = new bool[3];

    private Rigidbody rb;
    private Player player;

    public static bool DisablePlayerInput { get; set; }
    // Use this for initialization
    void Start()
    {
        player = GetComponent<Player>();
        mouseClickManager = new MouseClickManager(player);

        rb = GetComponentInChildren<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void Update()
    {
        if (DisablePlayerInput)
            return;
        
        scrollUD = Input.GetAxis(scrollWheel);
        forwardBack = Input.GetAxis(yMovement);
        leftRight = Input.GetAxis(xMovement);
        elevationChange = Input.GetAxis(elevation);
        jump = Input.GetKeyDown(jumpKey);

        flyToggle = Input.GetKeyDown(flyModeToggle);
        stopMoving = Input.GetKeyDown(removeVelocity);
        anyMouseButton = AnyMouseButtonPressed();
    }
    void FixedUpdate()
    {
        ManageInputs(new Vector3(0, 0, 0));
    }

    private void ManageInputs(Vector3 movement)
    {
        if (ShouldLookWhereCameraIsLooking())
        {
            playerMovement.UpdatePlayerRotation(forwardBack, leftRight, elevationChange);
        }

        if(leftRight == 0 && forwardBack == 0 && elevationChange == 0)
        {
            playerMovement.NormaliseRotation();
        }

        if (anyMouseButton)
        {
            mouseClickManager.OnMouseClick(mouseButtons, scrollUD);
        }

        if (flyToggle)
        {
            isFlying = !isFlying;
            rb.useGravity = !rb.useGravity;
            PlayerAnimation.IsFlying(!PlayerAnimation.IsFlyingVar);
        }

        if (IsMoving())
        {
            playerMovement.Move(isFlying, leftRight, forwardBack, elevationChange, jump);
            PlayerAnimation.InputYXZ(1f);
        }
        else
        {
            PlayerAnimation.InputYXZ(-1f);

        }

        if (stopMoving)
        {
            rb.velocity = stopVelocity;
            PlayerAnimation.InputYXZ(-1f);
        }

    }

    private bool ShouldLookWhereCameraIsLooking()
    {
        return forwardBack != 0 || leftRight != 0 || mouseButtons[0] || mouseButtons[2] || player.updateForwardDirection;
    }

    private bool AnyMouseButtonPressed()
    {
        mouseButtons[0] = Input.GetButtonDown(fire1);
        mouseButtons[1] = Input.GetButtonDown(fire2);
        mouseButtons[2] = Input.GetButtonDown(fire3);
        return mouseButtons[0] || mouseButtons[1] || mouseButtons[2] || scrollUD != 0;
    }

    private bool IsMoving()
    {
        return forwardBack != 0 || leftRight != 0 || elevationChange != 0 || jump;

    }

    private Vector3 MultiplyV3(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
    }

    
}
