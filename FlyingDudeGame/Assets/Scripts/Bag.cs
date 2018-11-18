using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Bag<T> 
{
    public T[] Elements { get; private set; }
    public int[] FreeIndexs { get; private set; }

    public int Capacity { get; private set; }
    public int NumElements { get; private set; }
    public int Index { get; private set; }

    public Bag(int size)
    {
        Capacity = size;
        Elements = new T[size];
        FreeIndexs = new int[size];
        NumElements = 0;
    }

    public void Add(T elem) {
        EnsureCapacity(Index);
        UnsafeAdd(elem);
    }

    public void UnsafeAdd(T elem)
    {
        if (FreeIndexs.Length == 0)
        {
            Elements[Index++] = elem;
        }
        else
        {
            Elements[FreeIndexs[FreeIndexs.Length - 1]] = elem;
        }
        NumElements++;
    }

    public T Get(int i)
    {
        return Elements[i];
    }

    public T Remove(int i)
    {
        T elem = Elements[i];
        Elements = RemoveAtCopy(i);
        return elem;
    }

    public T RemoveLast()
    {
        return Remove(NumElements - 1);
    }

    public void EnsureCapacity(int indexWanted)
    {
        if (indexWanted >= Capacity-1)
        {
            T[] newElm = new T[Mathf.Max(Capacity * 2, indexWanted)];
            Elements.CopyTo(newElm, 0);
            Elements = newElm;
        }
    }

    private T[] RemoveAtCopy(int index)
    {
        T[] dest = new T[Elements.Length - 1];
        if (index > 0)
            Array.Copy(Elements, 0, dest, 0, index);

        if (index < Elements.Length - 1)
            Array.Copy(Elements, index + 1, dest, index, Elements.Length - index - 1);

        return dest;
    }

}
