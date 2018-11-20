using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;

public abstract class Item : MonoBehaviour
{
    [HideInInspector]
    public int maxStackSize;
    public ItemData itemData;

    private void Start()
    {
        maxStackSize = itemData.maxStackSize;
    }

    public string GetToolTip()
    {
        return itemData.GetToolTipInfo();
    }

    public void SetItemData(ItemData newItemData)
    {
        itemData = newItemData;
        name = itemData.name;
    }
}