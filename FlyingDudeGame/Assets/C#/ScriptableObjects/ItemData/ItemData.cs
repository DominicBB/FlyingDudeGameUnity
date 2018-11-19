using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ItemData : ScriptableObject
{
    public new string name;
    public Image icon;
    public int maxStackSize;

    public string GetToolTipInfo()
    {
        return string.Format(
            "<b>{0}</b>\n" +
            "<i>MaxStackSize</i>: {1}", 
            name, 
            maxStackSize);
    }
}
