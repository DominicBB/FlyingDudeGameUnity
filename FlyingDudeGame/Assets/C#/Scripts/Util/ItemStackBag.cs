using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemStackBag
{
    private ItemStack<Item>[] itemStacks;
    public PriorityQueue<int> FreeIndexs { get; private set; }
    public PriorityQueue<int> UsedIndexs { get; private set; }

    public int Capacity { get; private set; }
    public int NumElements { get; private set; }
    public int BoundingIndex { get { return (UsedIndexs.Count == 0) ? 0: UsedIndexs.Peek(); } }

    public ItemStackBag(int capacity)
    {
        itemStacks = new ItemStack<Item>[capacity];
        FreeIndexs = new PriorityQueue<int>();
        for (int i = 0; i < capacity; i++)
        {
            FreeIndexs.EnQueue(i);
        }
        UsedIndexs = new PriorityQueue<int>(FunctionalComparer<int>.Create((x,y)=> x> y ? -1 : x < y ? 1 : 0));

        Capacity = capacity;
        NumElements = 0;
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
        int index = FreeIndexs.DeQueue();
        itemStacks[index] = elem;
        UsedIndexs.EnQueue(index);
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
            itemStacks[index] = itemStackToAdd;
            UsedIndexs.EnQueue(index);
            FreeIndexs.Remove(index);
            return true;
        }

        return UnsafeAdd(itemStackToAdd);

    }

    public ItemStack<Item> Get(int index)
    {
        return itemStacks[index];
    }

    public ItemStack<Item> Remove(int index)
    {
        if (index >= 0)
        {
            ItemStack<Item> itemStack = itemStacks[index];
            if (itemStack == null)
                return null;

            FreeIndexs.EnQueue(index);
            itemStacks[index] = null;
            UsedIndexs.Remove(index);
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
        return Remove(BoundingIndex);
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
        int finalI = BoundingIndex;
        for (int i = 0; i < finalI; i++)
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
        int finalI = BoundingIndex;
        for (int i = 0; i < finalI; i++)
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

    public List<ItemStack<Item>> ToList()
    {
        List<ItemStack<Item>> list = itemStacks.ToList<ItemStack<Item>>();
        list.TrimExcess();
        return list;
    }
}

internal class FunctionalComparer<T> : IComparer<T>
{
    private Func<T, T, int> comparer;
    public FunctionalComparer(Func<T, T, int> comparer)
    {
        this.comparer = comparer;
    }
    public static IComparer<T> Create(Func<T, T, int> comparer)
    {
        return new FunctionalComparer<T>(comparer);
    }
    public int Compare(T x, T y)
    {
        return comparer(x, y);
    }
}
