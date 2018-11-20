using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class InventoryContainer : MonoBehaviour
{
    private const int BG_INDEX = 0;
    private const int SEL_INDEX = 1;
    private const int ICON_INDEX = 2;

    private RectTransform rectTransform;

    public GameObject toolTip;

    public Inventory inventory;
    public ItemSlot[] itemSlots;

    private void Start()
    {
        toolTip = Instantiate(toolTip, transform.parent);
        rectTransform = GetComponent<RectTransform>();
        itemSlots = new ItemSlot[104];
        SetItemSlots();
    }

    private void SetItemSlots()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i] = rectTransform.GetChild(i).GetComponent<ItemSlot>();
            itemSlots[i].toolTip = toolTip;
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            Item item = inventory.GetItem(i);
            itemSlots[i].ItemData = (item == null)?null: item.itemData;
        }
    }

    public void ItemClicked(RectTransform buttonRectTransform)
    {
        if (!buttonRectTransform.GetChild(ICON_INDEX).gameObject.activeSelf)
            return;
        int index = buttonRectTransform.GetSiblingIndex();

        Item item = inventory.GetItem(index);
    }
}