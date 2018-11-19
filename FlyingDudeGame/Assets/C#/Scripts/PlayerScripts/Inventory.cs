using UnityEngine;
using System;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class Inventory : MonoBehaviour
{
    private ItemStackBag inventory;
    public int MaxCapacity { get; private set; }

    public GameObject inventoryWindow;

    // Use this for initialization
    void Start()
    {
        inventory = new ItemStackBag(MaxCapacity);
    }

   public Item GetItem(int index)
    {
        ItemStack<Item> itemStack = inventory.Get(index);
        return itemStack.Remove();
    }

    public bool AddItem(Item item, int amt)
    {
        ItemStack<Item> itemStack = GetItemStack(item.GetType());
        return Add(itemStack, amt);
    }

    public bool AddItem(Item item, int amt, int index)
    {
        ItemStack<Item> itemStack = inventory.Get(index);
        return Add(itemStack, amt);
    }

    public ItemStack<Item> GetItemStack(Type type)
    {
        return inventory.FindStackOfType(type);
    }

    private bool Add(ItemStack<Item> itemStack, int amt)
    {
        if (itemStack == null || amt < 1)
            return false;

       return itemStack.AddToStack(amt);
    }
}
