using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button exitButton;
    [SerializeField] private Button addButton;
    [SerializeField] private ScrollRect scrollRect;
    private int currentSlotCount = 0;
    [SerializeField] private int addcontentCount;
    [SerializeField] private GameObject itemSlot;

    [SerializeField] private List<UISlot> uiSlots;

    private void Start()
    {
        exitButton.onClick.AddListener(CloseInventory);
        addButton.onClick.AddListener(AddContentSize);
    }

    public void OpenInventory()
    {
        gameObject.SetActive(true);
    }

    public void CloseInventory()
    {
        gameObject.SetActive(false);
    }

    public void AddContentSize()
    {
        AddItem();
        currentSlotCount++;

        if (addcontentCount < currentSlotCount)
        {
            currentSlotCount = 1;
            scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, scrollRect.content.sizeDelta.y + 120);
        }
    }

    public void AddItem()
    {
        int random = Random.Range(0, GameManager.Instance.itemSOs.Count);
        ItemSO itemSO = GameManager.Instance.itemSOs[random];

        if (!itemSO.canEquip)
        {
            UISlot uiSlots = GetItemStack(itemSO);
            if (uiSlots != null)
            {
                uiSlots.amount++;
                uiSlots.amountText.text = uiSlots.amount.ToString();
                return;
            }
        }

        SetSlot(itemSO);
    }

    public void SetSlot(ItemSO itemSO)
    {
        GameObject slot = Instantiate(itemSlot, scrollRect.content);
        UISlot uiSlot = slot.GetComponent<UISlot>();
        uiSlots.Add(uiSlot);
        uiSlot.SetItem(itemSO);
        uiSlot.slotID = uiSlots.Count - 1;
    }

    public UISlot GetItemStack(ItemSO itemSO)
    {
        for(int i = 0; i < uiSlots.Count; i++)
        {
            if (uiSlots[i].item == itemSO && uiSlots[i].amount < itemSO.maxStack)
            {
                return uiSlots[i];
            }
        }
        return null;
    }
}
