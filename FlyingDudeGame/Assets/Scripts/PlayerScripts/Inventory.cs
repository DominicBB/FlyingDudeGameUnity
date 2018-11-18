using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Inventory : MonoBehaviour
{
    private Bag<ItemStack<Item>> inventory;
    public int MaxCapacity { get; private set; }
    // Use this for initialization
    void Start()
    {
        inventory = new Bag<ItemStack<Item>>(MaxCapacity);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //public bool AddItem(Item item)
    //{

    //}

    //public bool AddItem(int index)
    //{

    //}
}
