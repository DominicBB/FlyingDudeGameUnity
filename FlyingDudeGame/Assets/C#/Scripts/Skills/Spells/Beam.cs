using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : NonTargetedSpell
{
    public float speed;
    public float fireRate;
    public float distance;

    private Vector3 dir;
    private Vector3 end;
    private float maxTime;
    private float timer;

    public override void Initialise(Transform firePoint)
    {
        dir = firePoint.forward;
        end = transform.position + (dir * distance);
        maxTime = distance / speed;
        timer = 0.0f;
    }
       

    //private Transform firePoint;

 

    private void Update()
    {
        Debug.Log("timer, " + timer + " maxTime, " + maxTime);
        if (timer >= maxTime)
        {
            Destroy(gameObject);
        }
        else
        {
           
            transform.position = Vector3.Lerp(transform.position, end, speed * Time.deltaTime);
            timer += Time.deltaTime;
        }
        
        

    }


}
