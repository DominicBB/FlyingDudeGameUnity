using UnityEngine;
using System.Collections;

public class AimAtCrosshair : MonoBehaviour
{
    public Camera cam;
    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = cam.transform.forward;
    }
}
