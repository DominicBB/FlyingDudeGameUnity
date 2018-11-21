using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : NonTargetedSpell
{
    public float speedMultiplier;
    public float fireRate;
    public float maxDistance;

    private Vector3 dir;
    private Vector3 end;
    private Vector3 step;
   
    private float numSteps;
    private float stepCount;

    public override void Initialise(Transform firePoint)
    {

        dir = firePoint.forward;
        Vector3 distanceVec = (dir * maxDistance);
        end = transform.position + distanceVec;

        step = dir * speedMultiplier;
        numSteps = maxDistance / speedMultiplier;
        stepCount = 0;
    }

    private void Update()
    {
        if (stepCount >= numSteps)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position += (step * Time.deltaTime);
            stepCount += (1 * Time.deltaTime);
        }
    }


}
