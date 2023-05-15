using System;
using UnityEngine;

public class BeeDropSlot : MonoBehaviour
{
    public bool slotFilled => currentItem;
    public BeeItemBehaviour currentItem;
    
    public Bee PopBee()
    {
        try 
        {
            Bee bee = currentItem.inventorySlot.bee;
            currentItem = null;
            Destroy(transform.GetChild(1).gameObject);
            return bee;
        }
        catch (NullReferenceException) { return null; }
    }

    public Bee PeekBee()
    {
        try { return currentItem.inventorySlot.bee; }
        catch (NullReferenceException) { return null; }
    }
}