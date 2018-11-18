using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour {

    public GameObject skillHotBarIcon;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public abstract void Use(Transform firePoint);
    public abstract void Initialise(Transform firePoint);
}
