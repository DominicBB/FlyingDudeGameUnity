using System;
using System.Collections;
using System.Collections.Generic;

public class PriorityQueue<T> : ICollection<T>
    where T : IComparable
{
    private readonly IComparer<T> comparer;
    public PriorityQueue(IComparer<T> comparer )
    {
        this.comparer = comparer;
    }


    public PriorityQueue()
    {
        this.comparer = Comparer<T>.Default;
    }

    private List<T> elements = new List<T>();

    public int Count { get; private set; }

    public bool IsReadOnly { get; private set; }

    public void Add(T item)
    {
        throw new System.NotImplementedException();
    }

    public void Clear()
    {
        throw new System.NotImplementedException();
    }

    public bool Contains(T item)
    {
        throw new System.NotImplementedException();
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new System.NotImplementedException();
    }

    public bool Remove(T item)
    {
        int index = BinarySearch(item);

        if (IsInBounds(index)){
            elements.RemoveAt(index);
            Count--;
            return true;
        }
        return false;
    }

    public T Peek()
    {
        return elements[0];
    }

    public T DeQueue()
    {
        if(Count == 0)
        {
            throw new IndexOutOfRangeException();
        }
        T elem = elements[0];
        elements.RemoveAt(0);
        Count--;
        return elem;
    }

    public void EnQueue(T item)
    {
        elements.Insert( (Count==0)?0: BinaryInsert(item), item);
        Count++;
    }

    private int BinaryInsert(T item)
    {
        int middle = Count / 2;
        int left = 0;
        int right = Count;

        while (left <= right)
        {
            int val = comparer.Compare(item, elements[middle]);
            if (val > 0)
            {
                left = middle + 1;
                middle = (left + right) / 2;
            }
            else if (val < 0)
            {
                right = middle - 1;
                middle = (left + right) / 2;
            }
            else
            {
                return middle;
            }

            if(middle == Count)
            {
                return Count;
            }
        }
        return middle;
    }

    private int BinarySearch(T item)
    {
        int middle = Count / 2;
        int left = 0;
        int right = Count -1;

        while (left <= right)
        {
            int val = comparer.Compare(item, elements[middle]);
            if (val > 0)
            {
                left = middle + 1;
                middle = (left + right) / 2;
            }
            else if (val < 0)
            {
                right = middle - 1;
                middle = (left + right) / 2;
            }
            else
            {
                return middle;
            }
        }

        return -1;
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new System.NotImplementedException();
    }

    private bool IsInBounds(int i)
    {
        return i < Count && i >= 0;
    }
}
