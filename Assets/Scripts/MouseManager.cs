using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public static MouseManager instance;

    public Item heldItem;
    public Item GetHeldItem {  get { return heldItem; } }

    private void Awake()
    {
        instance = this;
    }

    public void UpdateHeldItem(UISlotHandler activeSlot)
    {
        var activeItem = activeSlot.item;

        if(heldItem != null && activeItem != null && heldItem.itemID == activeItem.itemID)
        {
            InventoryManager.instance.StackInInventory(activeSlot, heldItem);
            heldItem = null;
            return;
        }

        if(activeSlot.item != null)
        {
            InventoryManager.instance.ClearItemSlot(activeSlot);
        }
        if(heldItem != null)
            InventoryManager.instance.PlaceInInventory(activeSlot, heldItem);

        heldItem = activeItem;
    }

    public void PickupFromStack(UISlotHandler activeSlot)
    {
        if(heldItem == null)
        {
            heldItem = activeSlot.item.Clone();
            heldItem.itemAmt = 1;
        }
        else
        {
            heldItem.itemAmt++;
        }

        activeSlot.item.itemAmt--;
        activeSlot.itemCount.text = activeSlot.item.itemAmt.ToString();

        if(activeSlot.item.itemAmt <= 0)
        {
            InventoryManager.instance.ClearItemSlot(activeSlot);
        }
    }
}
