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

    public ItemStack<Item> ItemsOnCursor { get; private set; }

    // Use this for initialization
    void Start()
    {
        itemStackBag = new ItemStackBag(maxCapacity);
        SetTestInventory();
    }

    private void SetTestInventory()
    {
        TryAddToExistingStack(null, 1, ItemPool.LoadCoolRobe());
    }

    public Item GetItem(int index)
    {
        ItemStack<Item> itemStack = itemStackBag.Get(index);
        return (itemStack == null) ? null : itemStack.Peek();
    }

    public ItemStack<Item> GetItemStack(int index)
    {
        return itemStackBag.Get(index);
    }

    public ItemStack<Item> GetItemStack(Type type)
    {
        return itemStackBag.FindStackOfType(type);
    }

    public Item RemoveItem(int index)
    {
        ItemStack<Item> itemStack = itemStackBag.Get(index);
        return (itemStack == null) ? null : itemStack.Remove();
    }

    public bool AddItem(Item item, int amt)
    {
        ItemStack<Item> itemStack = GetItemStack(item.GetType());
        return TryAddToExistingStack(itemStack, amt, item);
    }

    public bool AddItem(Item item, int amt, int index)
    {
        ItemStack<Item> itemStack = itemStackBag.Get(index);
        return TryAddToExistingStack(itemStack, amt, item, index);
    }

    public bool PutItemOnCursor(ItemStack<Item> itemStack, int amt)
    {
        if (ItemsOnCursor == null)
        {
            ItemsOnCursor = itemStack.Split(amt);
            return true;
        }
        return false;
    }

    public bool PutItemOnCursor(Item item, int amt)
    {
        if (ItemsOnCursor == null)
        {
            ItemsOnCursor = new ItemStack<Item>(item, amt);
            return true;
        }
        return false;
    }

    public void RemoveItemFromCursor()
    {
        ItemsOnCursor = null;
    }

    private bool TryAddToExistingStack(ItemStack<Item> itemStack, int amt, Item item)
    {
        if (amt < 1)
        {
            return false;
        }

        if (itemStack == null)
        {
            if (!itemStackBag.AtMaxCapacity())
            {
                ItemStack<Item> newItemStack = CreateItemStack(item, amt);
                return AddNewStackToBag(newItemStack);
            }
            return false;
        }
        return itemStack.AddToStack(amt);
    }

    private bool TryAddToExistingStack(ItemStack<Item> itemStack, int amt, Item item, int index)
    {
        if (amt < 1)
        {
            return false;
        }

        if (itemStack == null)
        {
            if (!itemStackBag.AtMaxCapacity())
            {
                ItemStack<Item> newItemStack = CreateItemStack(item, amt);
                return AddNewStackToBag(newItemStack, index);
            }
            return false;
        }
        return itemStack.AddToStack(amt);
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < itemStackBag.BoundingIndex; i++)
        {
            UpdateInventory(i);
        }
    }

    public void UpdateInventory(int index)
    {
        ItemStack<Item> itemStack = itemStackBag.Get(index);
        if ((itemStack == null) ? false : itemStack.StackSize <= 0)
            itemStackBag.Remove(index);
    }

    private ItemStack<Item> CreateItemStack(Item item, int amt)
    {
        return new ItemStack<Item>(item, amt);
    }

    private bool AddNewStackToBag(ItemStack<Item> itemStack)
    {
        return itemStackBag.UnsafeAdd(itemStack);
    }

    private bool AddNewStackToBag(ItemStack<Item> itemStack, int index)
    {
        return itemStackBag.Put(itemStack, index);
    }


}
