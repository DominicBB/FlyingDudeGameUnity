using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour{

    public GameObject skillHotBar;
    public GameObject crossHair;
  
    // Use this for initialization
    void Start () {
	    	
	}
	
	public void AddSkillToSkillBar(Skill skill)
    {
        Instantiate(skill.skillHotBarIcon, skillHotBar.transform);
    }

    public void RemoveSkillToSkillBar(Skill skill)
    {
        Destroy(skill.skillHotBarIcon);
    }

    
}
