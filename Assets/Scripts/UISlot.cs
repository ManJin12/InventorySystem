using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    public ItemSO item;

    public Button button;
    public Image icon;
    public Image equipIcon;
    public TextMeshProUGUI amountText;

    public int slotID;
    public bool equiped = false;
    public int amount;

    public UIInventory inventory;

    private void Start()
    {
        button.onClick.AddListener(OnClickButtion);
    }

    public void SetItem(ItemSO newItem)
    {
        item = newItem;
        icon.sprite = item.itemImage;
        button.GetComponent<Image>().sprite = item.slotImage;
        amount = 1;
        amountText.text = item.canEquip ? string.Empty : amount.ToString();
    }
    public void OnClickButtion()
    {
        inventory.InteractionSlot(slotID);
    }
}