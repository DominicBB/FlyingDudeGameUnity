using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public static class ItemPool 
{
    private static List<Item> itemPool = new List<Item>() {
        Resources.Load<Robe>("Robe") };

    public static Robe LoadCoolRobe()
    {
        return (Robe) itemPool[0];
    }
}
