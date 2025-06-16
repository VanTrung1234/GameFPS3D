using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public Image icon;
    public Button equipButton;
    private Item currentItem;

    public void Setup(Item item, System.Action<Item> onEquip)
    {
        currentItem = item;
        icon.sprite = item.icon;
        equipButton.onClick.RemoveAllListeners();
        equipButton.onClick.AddListener(() => onEquip?.Invoke(currentItem));
    }
}
