using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class SkillHotBar : MonoBehaviour
{


    private List<Skill> skills = new List<Skill>();
    private List<Skillicon> skillIcons = new List<Skillicon>();

    [HideInInspector]
    public Skill selected;
    private Skillicon selectedIcon;

    public GameObject hotBarPanel;

    private int currentIndex;

    private void Start()
    {
        hotBarPanel = gameObject;
    }

    public void AddSkill(Skill skill)
    {
        Skillicon skillIcon = Instantiate(skill.skillHotBarIcon, hotBarPanel.transform).GetComponentInChildren<Skillicon>();

        if (selected == null)
        {
            selected = skill;
            skillIcon.Select();
            selectedIcon = skillIcon;
        }
        skillIcons.Add(skillIcon);
        skills.Add(skill);
    }

    public void RemoveSkill(Skill skill)
    {
        RemoveSkill(skills.IndexOf(skill));
    }

    public void RemoveSkill(int index)
    {
        skills.RemoveAt(index);
        skillIcons.RemoveAt(index);

        if (index <= currentIndex)
        {
            currentIndex = (currentIndex == 0) ? 0 : currentIndex - 1;
        }
    }

    public void Cycle(float scrollDir)
    {
        if (scrollDir > 0)
        {
            int nextIndex = NextIndexRight();
            SelectNewSkill(skills[nextIndex], skillIcons[nextIndex]);
        }
        else if (scrollDir < 0)
        {
            int nextIndex = NextIndexLeft();
            SelectNewSkill(skills[nextIndex], skillIcons[nextIndex]);
        }
    }

    private void SelectNewSkill(Skill skill, Skillicon skillIcon)
    {
        selectedIcon.DeSelect();
        selectedIcon = skillIcon;
        selectedIcon.Select();

        selected = skill;
    }

    private int NextIndexLeft()
    {
        if (currentIndex - 1 < 0)
        {
            return skills.Count - 1;
        }

        return --currentIndex;
    }

    private int NextIndexRight()
    {
        if (currentIndex + 1 > skills.Count - 1)
        {
            return 0;
        }

        return ++currentIndex;
    }
}
