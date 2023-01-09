using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        None,
        Stick,
        Food,
        Medicine,
        SpaceHelmet
    }

    public ItemType itemType;
    public int index;
    public int amout;

    public bool IsHoldable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Stick:
            case ItemType.Food:
            case ItemType.Medicine: 
                return true;

            case ItemType.SpaceHelmet:
                return false;
        }
    }
}
