using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISlot : MonoBehaviour
{
    public int slotID;
    public ItemSO item;
    public int amount;
    public bool isEmpty = true;
    public void AddItem(ItemSO newItem, int newAmount)
    {
        item = newItem;
        amount = newAmount;
        isEmpty = false;
    }
    public void RemoveItem()
    {
        item = null;
        amount = 0;
        isEmpty = true;
    }
}
