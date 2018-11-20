using UnityEngine;
using System.Collections;
using System;

public class InventoryWindow : MonoBehaviour
{
    private KeyCode inventoryKey = KeyCode.I;
    private bool inventoryOpen;

    public GameObject inventoryWindow;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            ToggleInventory();
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
        Cursor.visible = true;
        PlayerInputManager.DisablePlayerInput = false;
        inventoryOpen = false;
    }

    public void OnTabChange(Transform transform)
    {

    }
}
