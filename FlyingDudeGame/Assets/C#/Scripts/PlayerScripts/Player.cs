using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpellBook allSpells;

    private Animator animator;
    public Transform firePoint;
    public bool updateForwardDirection;

    public SkillHotBar skillHotBar; 
    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        PlayerAnimation.Animator = animator;
        PlayerAnimation.Player = GetComponent<Player>();

        allSpells = GetComponentInChildren<SpellBook>();
        PopulateSkillBar();
    }

    private void PopulateSkillBar()
    {
        skillHotBar.AddSkill(allSpells.allSpells[0]);
        skillHotBar.AddSkill(allSpells.allSpells[0]);
        skillHotBar.AddSkill(allSpells.allSpells[0]);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FireAttack()
    {
        Skill skill = skillHotBar.selected;
        skill = Instantiate(skill, firePoint.position, Quaternion.identity);
        skill.Initialise(firePoint);
        updateForwardDirection = false;
    }

    public void AttackEnded()
    {
        PlayerAnimation.AttackEnded();
    }


    public void UpdateSelectedSkill(float scrollUD)
    {
        skillHotBar.Cycle(scrollUD);
    }


    


}
   
