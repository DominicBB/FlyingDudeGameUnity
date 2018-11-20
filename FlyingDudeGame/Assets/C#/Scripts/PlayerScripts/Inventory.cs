using UnityEngine;
using System;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;


public class Inventory : MonoBehaviour
{
    private ItemStackBag itemStackBag;
    private int maxCapacity = 104;
    public int MaxCapacity { get; private set; }

    public GameObject inventoryWindow;

    // Use this for initialization
    void Start()
    {
        itemStackBag = new ItemStackBag(maxCapacity);
        SetTestInventory();
    }

    private void SetTestInventory()
    {   
        AddToStack(null, 1, ItemPool.LoadCoolRobe());
    }

    public Item GetItem(int index)
    {
        ItemStack<Item> itemStack = itemStackBag.Get(index);
        return (itemStack == null)?null: itemStack.Peek();
    }

    public Item RemoveItem(int index)
    {
        ItemStack<Item> itemStack = itemStackBag.Get(index);
        return (itemStack == null) ? null : itemStack.Remove();
    }

    public bool AddItem(Item item, int amt)
    {
        ItemStack<Item> itemStack = GetItemStack(item.GetType());
        return AddToStack(itemStack, amt, item);
    }

    public bool AddItem(Item item, int amt, int index)
    {
        ItemStack<Item> itemStack = itemStackBag.Get(index);
        return AddToStack(itemStack, amt, item);
    }

    public ItemStack<Item> GetItemStack(Type type)
    {
        return itemStackBag.FindStackOfType(type);
    }

    private bool AddToStack(ItemStack<Item> itemStack, int amt, Item item)
    {
        if(amt < 1)
        {
            return false;
        }

        if (itemStack == null)
        {
            if (!itemStackBag.AtMaxCapacity())
            {
                ItemStack<Item> newItemStack = CreateItemStack(item, amt);
                return AddStackToBag(newItemStack);
            }
            return false;
        }
       return itemStack.AddToStack(amt);
    }

    private ItemStack<Item> CreateItemStack(Item item, int amt)
    {
        return new ItemStack<Item>(item, amt);
    }

    private bool AddStackToBag(ItemStack<Item> itemStack)
    {
        return itemStackBag.UnsafeAdd(itemStack);
    }
}
