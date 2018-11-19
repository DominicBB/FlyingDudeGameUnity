using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections;

public abstract class Item : MonoBehaviour
{
    public int maxStackSize;
    public ItemData itemData;

    private void Start()
    {
        maxStackSize = itemData.maxStackSize;
    }


}