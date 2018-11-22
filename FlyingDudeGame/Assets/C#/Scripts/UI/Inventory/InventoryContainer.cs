using UnityEngine;
using UnityEngine.UI;

public class InventoryContainer : MonoBehaviour
{
    private const int BG_INDEX = 0;
    private const int SEL_INDEX = 1;
    private const int ICON_INDEX = 2;

    private RectTransform rectTransform;
    private CursorItem cursorItem;

    public GameObject toolTip;
    public Inventory inventory;
    public ItemSlot[] itemSlots;
    private SplitSlider splitSlider;
    public GameObject splitSliderObj;
    private bool shift;

    private void Start()
    {
        toolTip = Instantiate(toolTip, transform.parent);
        splitSlider = Instantiate(splitSliderObj, transform.parent).GetComponent<SplitSlider>();
        rectTransform = GetComponent<RectTransform>();
        itemSlots = new ItemSlot[104];
        SetItemSlots();

        InventoryWindow inventoryWindow = GetComponentInParent<InventoryWindow>();
        cursorItem = inventoryWindow.cursorItem;
    }

    private void SetItemSlots()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            ItemSlot itemSlot = rectTransform.GetChild(i).GetComponent<ItemSlot>();
            itemSlots[i] = itemSlot;
            Button button = itemSlot.GetComponentInChildren<Button>();
            button.onClick.AddListener(delegate { ItemClicked(itemSlot.GetComponent<RectTransform>()); });
            itemSlot.toolTip = toolTip;
        }
    }

    private void OnEnable()
    {
        RefreshItemSlots();
    }

    public void ItemClicked(RectTransform buttonRectTransform)
    {
        int slotIndex = buttonRectTransform.GetSiblingIndex();
        //place items form cursor into empty slot
        if (IsItemSlotEmpty(buttonRectTransform))
        {
            if (CursorItem.IsItemOnCursor)
            {
                ItemStack<Item> itemStack = inventory.ItemsOnCursor;
                inventory.AddItem(itemStack.Peek(), itemStack.StackSize, slotIndex);
                RemoveFromCursor();
            }
        }

        else
        {
            //Add items to cursor
            if (!CursorItem.IsItemOnCursor)
            {
                if (HoldingShift())
                    DeploySplitSlider(buttonRectTransform, slotIndex);
                else
                    AddStackToCursor(slotIndex, -1);
            }
            //put items into stack if applicable
            else if (cursorItem.itemOnCursor.SameStackType(inventory.GetItemStack(slotIndex)))
            {
                inventory.GetItemStack(slotIndex).AddToStack(cursorItem.itemOnCursor.StackSize);
                RemoveFromCursor();
            }
        }
        UpdateInventory(slotIndex);
    }

    private void DeploySplitSlider(RectTransform buttonRectTransform, int slotIndex)
    {
        int maxAmt = inventory.GetItemStack(slotIndex).StackSize;
        splitSlider.EnableSlider(buttonRectTransform, maxAmt, slotIndex);
    }

    public void OnSpliderAccept(int value, int slotIndex)
    {
        AddStackToCursor(slotIndex, value);
    }

    private void UpdateInventory()
    {
        inventory.UpdateInventory();
        RefreshItemSlots();

    }
    private void UpdateInventory(int index)
    {
        inventory.UpdateInventory(index);
        RefreshItemSlot(index);
    }

    private void RefreshItemSlots()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            RefreshItemSlot(i);
        }
    }

    private void RefreshItemSlot(int index)
    {
        Item item = inventory.GetItem(index);
        itemSlots[index].ItemData = (item == null) ? null : item.itemData;

    }

    private bool IsItemSlotEmpty(RectTransform buttonRectTransform)
    {
        return !buttonRectTransform.GetChild(ICON_INDEX).gameObject.activeSelf;
    }

    private void AddStackToCursor(int slotIndex, int amt)
    {
        ItemStack<Item> itemStack = inventory.GetItemStack(slotIndex);
        inventory.PutItemOnCursor(itemStack, (amt == -1) ? itemStack.StackSize : amt);
        cursorItem.PutItemOnCursor(inventory.ItemsOnCursor, slotIndex);
    }

    private void RemoveFromCursor()
    {
        inventory.RemoveItemFromCursor();
        cursorItem.RemoveItem();
    }

    private bool HoldingShift()
    {
        return shift;
    }

    private void OnGUI()
    {
        shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }
}