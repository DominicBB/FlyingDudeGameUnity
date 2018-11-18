using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Transform playerTransfom;
    private Camera cam;

    public float horizontalMoveSpeedFlying;
    public float horizontalMoveSpeedWalk;
    public float verticalMoveSpeedFlying;
    public float verticalMoveSpeedWalk;
    public float elevationChangeSpeed;
    public float maxWalkAngle;
    public float normRotSpeed;

    private const float jumpTime = 2f;
    private float jumpTimeElapsed = 0f;
    private const float jumpForceMax = 10f;
    private const float jumpForceStep = (jumpForceMax / 4) / jumpTime;
    private float jumpForce = 0f;
    private bool jumping;

    public bool Grounded { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTransfom = GetComponent<Transform>();
        cam = Camera.main;
    }

    internal void Move(bool isFlying, float leftRight, float forwardBack, float elevationChange, bool jump)
    {
        if (jumping)
        {
            jump = false;
        }

        Vector3 movement = new Vector3(0, 0, 0);

        if (isFlying)
        {
            Grounded = false;
            movement = FlyingControls(movement, leftRight, forwardBack, elevationChange);
        }
        else
        {
            if (jump)
            {
                StartCoroutine(Jump());
            }
            movement = WalkingControls(movement, leftRight, forwardBack);
        }

        rb.AddForce(movement);

        RotatePlayer(leftRight, forwardBack);
    }

    private IEnumerator Jump()
    {
        jumping = true;
        rb.velocity += (playerTransfom.up * jumpForceMax);
        Grounded = false;

        while (!Grounded)
        {
            jumpTimeElapsed += Time.deltaTime;
            rb.velocity += (playerTransfom.up * jumpForce * jumpTimeElapsed);
            jumpForce -= jumpForceStep * Time.deltaTime;
            yield return null;
        }

        jumpTimeElapsed = 0;
        jumpForce = 0f;
        jumping = false;
    }

    private Vector3 WalkingControls(Vector3 movement, float leftRight, float forwardBack)
    {
        return ComputeMovementForces(movement, leftRight, horizontalMoveSpeedWalk, forwardBack, verticalMoveSpeedWalk);
    }

    private Vector3 FlyingControls(Vector3 movement, float leftRight, float forwardBack, float elevationChange)
    {
        movement += (playerTransfom.up * elevationChange * (elevationChangeSpeed * Time.deltaTime));
        return ComputeMovementForces(movement, leftRight, horizontalMoveSpeedFlying, forwardBack, verticalMoveSpeedFlying);
    }

    private Vector3 ComputeMovementForces(Vector3 movement, float leftRight, float lrForce, float forwardBack, float fbForce)
    {
        movement += (playerTransfom.right * leftRight * (lrForce * Time.deltaTime));
        movement += (playerTransfom.forward * forwardBack * (fbForce * Time.deltaTime));
        return movement;
    }

    private void RotatePlayer(float leftRight, float forwardBack)
    {
        Vector3 lrComponent = transform.right * leftRight;
        Vector3 fbComponent = transform.forward * forwardBack;

        if (leftRight != 0 || forwardBack != 0)
        {
            transform.forward = lrComponent + fbComponent;
        }
    }

    public void UpdatePlayerRotation(float leftRight, float forwardBack, float elevationChange)
    {
        if (Grounded)
        {
            Vector3 projected = ProjectionOntoPlane(transform.up, cam.transform.forward);
            transform.forward = projected;
        }
        else
        {
            rb.transform.forward = cam.transform.forward;
        }
    }

    public void NormaliseRotation()
    {
        if (transform.up == Vector3.up || Grounded)
        {
            return;
        }

        Vector3 axis = Vector3.Cross(transform.up, Vector3.up);
        transform.RotateAround(transform.position, axis, normRotSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        AlignWithGround(collision);
        Grounded = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        AlignWithGround(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        Grounded = false;
    }

    private void AlignWithGround(Collision collision)
    {
        Vector3 col_n = collision.contacts[0].normal;
        if(Vector3.Angle(col_n, Vector3.up) < maxWalkAngle)
        {
            transform.forward = Vector3.Cross(transform.right, col_n);
        }
        
    }

    private Vector3 ProjectionOntoPlane(Vector3 p_n, Vector3 vector)
    {
        return Vector3.Cross(p_n, Vector3.Cross(vector, p_n));
    }
}
