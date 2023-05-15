using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "BeeBreedingPrototype/Inventory")]
[Serializable]
public class Inventory : ScriptableObject
{
    public List<InventorySlot> slots;

    public void AddBee(Bee bee)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.bee.Equals(bee))
            {
                slot.amount++;
                return;
            }
        }
        slots.Add(new InventorySlot(bee));
    }

    public void RemoveBee(Bee bee)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.bee == bee)
            {
                slot.amount--;
                if (slot.amount == 0) slots.Remove(slot);
                break;
            }
        }
    }

    public void Reset()
    {
        slots = new List<InventorySlot>();
    }
}

[Serializable]
public class InventorySlot
{
    public Bee bee;
    public int amount;

    public InventorySlot(Bee bee)
    {
        this.bee = bee;
        this.amount = 1;
    }
}