
using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    public float distanceFromTarget = 10f;
    private Camera cam;

    private float lookLR;
    private float lookUD;
    public float sensitivtyLR = 4f;
    public float sensitivtyUD = 4f;

    private const float YMAXROT = 15;
    private const float YMINROT = -15;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (PlayerInputManager.DisablePlayerInput)
            return;

        float dTime = Time.deltaTime;
        UpdateRotationAmts();
    }

    private void FixedUpdate()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        cam.transform.rotation = CalculateRotation();
        Vector3 distanceVec = CameraOcclusion();
        UpdateCameraPos(distanceVec);
    }

    private Quaternion CalculateRotation()
    {
        return Quaternion.Euler(lookUD * sensitivtyUD, lookLR * sensitivtyLR, 0);
    }

    private Vector3 CameraOcclusion()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(target.position, -cam.transform.forward, out hitInfo, distanceFromTarget))
        {
            return new Vector3(0, 0, -hitInfo.distance);
        }

        return new Vector3(0, 0, -distanceFromTarget);
    }

    private void UpdateCameraPos(Vector3 distanceVec)
    {
        cam.transform.position = (target.position) + cam.transform.rotation * distanceVec;
        cam.transform.LookAt(target.position);
    }

    private void UpdateRotationAmts()
    {
        lookLR += Input.GetAxis("Mouse X");
        lookUD += -Input.GetAxis("Mouse Y");

        if (lookUD > YMAXROT)
        {
            lookUD = YMAXROT;
        }
        else if (lookUD < YMINROT)
        {
            lookUD = YMINROT;
        }
    }
}
