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
    public int amount;


    public void SetItem()
    {
        item = GetItem();
        icon.sprite = item.itemImage;
        button.GetComponent<Image>().sprite = item.slotImage;
        amountText.text = item.canEquip ? string.Empty : amount.ToString();
    }

    private ItemSO GetItem()
    {
        int itemCount = Random.Range(0, GameManager.Instance.itemSOs.Count);
        ItemSO item = GameManager.Instance.itemSOs[itemCount];
        return item;
    }
}
