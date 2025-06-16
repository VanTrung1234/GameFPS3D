using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public Transform contentPanel;           // Gán Content của Scroll View vào đây
    public GameObject itemSlotPrefab;        // Prefab ItemSlot
    public List<Item> allItems;              // Gán sẵn các item trong Inspector

    // 4 slot trang bị:
    public Image mainGunSlot;
    public Image subGunSlot;
    public Image grenadeSlot;
    public Image smokeSlot;

    void Start()
    {
        PopulateInventory();
    }

    void PopulateInventory()
    {
        foreach (var item in allItems)
        {
            GameObject slotObj = Instantiate(itemSlotPrefab, contentPanel);
            ItemSlot slot = slotObj.GetComponent<ItemSlot>();
            slot.Setup(item, EquipItem);
        }
    }
    public void thaosung1()
    {
        mainGunSlot.sprite = null;
    }
    public void thaosung2()
    {
        subGunSlot.sprite = null;
    }
    public void thaogrenadeSlot()
    {
        grenadeSlot.sprite = null;
    }
    public void thaosmokeSlot()
    {
        smokeSlot.sprite = null;
    }
    void EquipItem(Item item)
    {
        switch (item.itemType)
        {
            case ItemType.MainGun:
                mainGunSlot.sprite = item.icon;
                break;
            case ItemType.SubGun:
                subGunSlot.sprite = item.icon;
                break;
            case ItemType.Grenade:
                grenadeSlot.sprite = item.icon;
                break;
            case ItemType.Smoke:
                smokeSlot.sprite = item.icon;
                break;
        }
    }
}
