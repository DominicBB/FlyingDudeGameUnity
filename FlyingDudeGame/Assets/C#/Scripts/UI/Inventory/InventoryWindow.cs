using UnityEngine;
using System.Collections;
using System;

public class InventoryWindow : MonoBehaviour
{
    private KeyCode inventoryKey = KeyCode.I;
    private bool inventoryOpen;

    public GameObject inventoryWindow;
    public CursorItem cursorItem;

    private void Start()
    {
        cursorItem = Instantiate<CursorItem>(cursorItem, gameObject.GetComponent<RectTransform>());
        cursorItem.GetComponent<RectTransform>().anchorMin =Vector2.zero;
        cursorItem.GetComponent<RectTransform>().anchorMax = Vector2.zero;
    }
    void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            ToggleInventory();
        }

        if (CursorItem.IsItemOnCursor)
        {
            cursorItem.UpdateCursorItemPosition(Input.mousePosition);
        }
    }

    public void ToggleInventory()
    {
        if (!inventoryOpen)
            Open();
        else
            Close();
    }

    public void Open()
    {
        inventoryWindow.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PlayerInputManager.DisablePlayerInput = true;
        inventoryOpen = true;
    }

    public void Close()
    {
        inventoryWindow.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PlayerInputManager.DisablePlayerInput = false;
        inventoryOpen = false;
    }

    public void OnTabChange(Transform transform)
    {

    }
}
