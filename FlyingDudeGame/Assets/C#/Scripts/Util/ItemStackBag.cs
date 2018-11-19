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

    public void Add(ItemStack<Item> elem)
    {
        EnsureCapacity(BoundingIndex);
        UnsafeAdd(elem);
    }

    public void UnsafeAdd(ItemStack<Item> elem)
    {
        if (FreeIndexs.Count == 0)
        {
            itemStacks[BoundingIndex++] = elem;
        }
        else
        {
            itemStacks[FreeIndexs.RemoveAndGet<int>(FreeIndexs.Count - 1)] = elem;
        }
        NumElements++;
    }

    public ItemStack<Item> Get(int i)
    {
        return itemStacks[i];
    }
    
    public ItemStack<Item> Remove(int i)
    {
        if( i != -1)
        {
            FreeIndexs.Add(i);
        }
        return (i == -1) ? null : itemStacks[i];
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
       for(int i = 0; i<BoundingIndex; i++)
        {
            if(itemStacks[i].GetStackType() == type)
            {
                return i;
            }
        }
        return -1;
    }

}
