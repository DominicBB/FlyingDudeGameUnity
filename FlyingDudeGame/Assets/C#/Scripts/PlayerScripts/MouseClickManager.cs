using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickManager {
    private Player player;

    public MouseClickManager(Player player)
    {
        this.player = player;
    }

    public void OnMouseClick(bool[] mouseBottuns, float scrollUD)
    {
        if (mouseBottuns[0])
        {
            PlayerAnimation.Attack(true);
        }

        if(scrollUD != 0)
        {
            player.UpdateSelectedSkill(scrollUD);
        }
    }

}
