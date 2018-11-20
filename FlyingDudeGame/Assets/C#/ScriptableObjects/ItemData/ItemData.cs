using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ItemData : ScriptableObject
{
    public new string name;
    public Sprite icon;
    public int maxStackSize = 1;

    public string GetToolTipInfo()
    {
        return string.Format(
            "<b>{0}</b>\n" +
            "<i>MaxStackSize</i>: {1}", 
            name, 
            maxStackSize);
    }
}
