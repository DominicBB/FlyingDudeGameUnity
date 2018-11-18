using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class ItemStack<T>
    where T : Item
{
    private T item;
    public int StackSize { get; private set; }

    public ItemStack(T item)
    {
        this.item = item;
        StackSize++;
    }

    public T Remove()
    {
        return (StackSize > 0) ? item : null;
    }

    public List<T> Remove(int amtToRemove)
    {
        List<T> items = new List<T>();
        for(int i = 0; i< amtToRemove; i++)
        {
            if(StackSize-- > 0)
            {
                items.Add(item);
            }
            else
            {
                break;
            }
        }

        return items;
    }

    public Type GetStackType()
    {
        return typeof(T);
    }
}
