using System;

public class ItemStack<T>
    where T : Item
{
    private T item;
    public int StackSize { get; private set; }
    public int MaxStackSize { get; private set; }

    public ItemStack(T item, int amt)
    {
        this.item = item;
        MaxStackSize = item.maxStackSize;
        StackSize += amt;
    }

    public bool AddToStack(int amt)
    {
        if (StackSize > MaxStackSize)
        {
            return false;
        }

        StackSize += (MaxStackSize - amt >= StackSize) ? amt : MaxStackSize - StackSize;
        return true;
    }

    public T Remove()
    {
        return (StackSize-- > 0) ? item : null;
    }

    public T Peek()
    {
        return (StackSize > 0) ? item : null;
    }

    public ItemStack<T> RemoveAll()
    {
        ItemStack<T> itemStack = new ItemStack<T>(Peek(), 0);

        itemStack.AddToStack(StackSize);
        StackSize -= StackSize;

        return itemStack;
    }


    public ItemStack<T> Split(int amtToRemove)
    {
        ItemStack<T> itemStack = new ItemStack<T>(Peek(), 0);

        int dif = StackSize - amtToRemove;

        if (dif < 0)
        {
            itemStack.AddToStack(dif);
            StackSize -= dif;
        }
        else
        {
            itemStack.AddToStack(amtToRemove);
            StackSize -= amtToRemove;
        }
        return itemStack;
    }

    public Type GetStackType()
    {
        return typeof(T);
    }

    public bool SameStackType(ItemStack<Item> itemStack)
    {
        return GetStackType() == itemStack.GetStackType();
    }
}
