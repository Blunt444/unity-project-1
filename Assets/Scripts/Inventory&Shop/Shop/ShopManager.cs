using UnityEngine;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopSlot[] shopSlots;

    [SerializeField] private InventoryManager inventoryManager;

    public void PopulateShopItems(List<ShopItems> shopItems)
    {

        int activeSlots = 0;

        for (int i = 0; i < shopItems.Count; i++)
        {
            if(activeSlots >= shopSlots.Length) break;
            if (shopItems[i] == null || shopItems[i].itemSO == null) continue;
            ShopItems shopItem = shopItems[i];
            shopSlots[i].Initialize(shopItem.itemSO, shopItem.price);
            shopSlots[i].gameObject.SetActive(true);
            activeSlots++;
        }
        for (int i = activeSlots; i < shopSlots.Length; i++)
        {
            shopSlots[i].gameObject.SetActive(false);
        }
    }

    public void SellItems(ItemSO itemSO)
    {
        if (itemSO == null) return;

        foreach (ShopSlot slot in shopSlots)
        {
            if (slot.itemSO == itemSO)
            {
                inventoryManager.gold += slot.price / 2;
                inventoryManager.goldText.text = inventoryManager.gold.ToString();
                return;
            }
        }
    }

    public void TryBuyItem(ItemSO itemSO, int price)
    {
        if (itemSO == null || inventoryManager.gold < price) return;
        if (HasSpaceForItem(itemSO))
        {
            inventoryManager.gold -= price;
            inventoryManager.goldText.text = inventoryManager.gold.ToString();
            inventoryManager.AddItem(itemSO, 1);
        }
    }

    private bool HasSpaceForItem(ItemSO itemSO)
    {
        foreach (InventorySlot slot in inventoryManager.inventorySlots)
        {
            if (slot.itemSO == itemSO && slot.quantity < itemSO.stackSize) return true;
            else if (slot.itemSO == null) return true;
        }
        return false;
    }
}

[System.Serializable]
public class ShopItems
{
    public ItemSO itemSO;
    public int price;
}
