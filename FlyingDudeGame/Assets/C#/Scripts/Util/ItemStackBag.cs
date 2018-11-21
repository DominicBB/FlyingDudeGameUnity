using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
using ListExtensions;

public class ItemStackBag
{
    private ItemStack<Item>[] ItemStacks
    {
        get
        {
            return itemStacks;
        }
    }

    private ItemStack<Item>[] itemStacks;
    public List<int> FreeIndexs { get; private set; }
    public int Capacity { get; private set; }
    public int NumElements { get; private set; }
    public int BoundingIndex { get; private set; }

    public ItemStackBag(int capacity)
    {
        itemStacks = new ItemStack<Item>[capacity];
        FreeIndexs = new List<int>();
        Capacity = capacity;
        NumElements = 0;
        BoundingIndex = 0;
    }

    public bool Add(ItemStack<Item> elem)
    {
        EnsureCapacity(BoundingIndex);
        return UnsafeAdd(elem);
    }

    public bool UnsafeAdd(ItemStack<Item> elem)
    {
        if (AtMaxCapacity())
        {
            return false;
        }
        if (FreeIndexs.Count == 0)
        {
            itemStacks[BoundingIndex++] = elem;
        }
        else
        {
            itemStacks[FreeIndexs.RemoveAndGet<int>(FreeIndexs.Count - 1)] = elem;
        }
        NumElements++;
        return true;
    }

    public bool Put(ItemStack<Item> itemStackToAdd, int index)
    {
        if (AtMaxCapacity())
            return false;
        
        ItemStack<Item> itemStack = Get(index);

        if (itemStack == null)
        {
            ItemStacks[index] = itemStackToAdd;
            return true;
        }

        return UnsafeAdd(itemStackToAdd);

    }

    public ItemStack<Item> Get(int i)
    {
        return itemStacks[i];
    }

    public ItemStack<Item> Remove(int i)
    {
        if (i != -1)
        {
            FreeIndexs.Add(i);
            ItemStack<Item> itemStack = itemStacks[i];
            itemStacks[i] = null;
            NumElements--;
            return itemStack;
        }

        return null;
    }

    public ItemStack<Item> RemoveOfType(Type type)
    {
        int i = FindIndexOfType(type);
        return Remove(i);
    }

    public ItemStack<Item> RemoveLast()
    {
        return Remove(BoundingIndex--);
    }

    public void EnsureCapacity(int indexWanted)
    {
        if (indexWanted >= Capacity - 1)
        {
            ItemStack<Item>[] newElm = new ItemStack<Item>[Mathf.Max(Capacity * 2, indexWanted)];
            itemStacks.CopyTo(newElm, 0);
            itemStacks = newElm;
        }
    }

    public ItemStack<Item> FindStackOfType(Type type)
    {
        for (int i = 0; i < BoundingIndex; i++)
        {
            if (itemStacks[i].GetStackType() == type)
            {
                return itemStacks[i];
            }
        }
        return null;
    }

    public int FindIndexOfType(Type type)
    {
        for (int i = 0; i < BoundingIndex; i++)
        {
            if (itemStacks[i].GetStackType() == type)
            {
                return i;
            }
        }
        return -1;
    }

    public bool AtMaxCapacity()
    {
        return Capacity == NumElements;
    }

}
