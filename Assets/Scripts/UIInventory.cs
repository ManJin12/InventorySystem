using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button exitButton;
    [SerializeField] private Button addButton;
    [SerializeField] private Button equipButton;
    [SerializeField] private TextMeshProUGUI equipText;

    [SerializeField] private ScrollRect scrollRect;
    private int currentSlotCount = 0;
    [SerializeField] private int addcontentCount;
    [SerializeField] private GameObject itemSlot;

    [SerializeField] private List<UISlot> uiSlots;

    [SerializeField] private List<ItemSO> inventory;

    [Header("SelectedItem")]
    [SerializeField] private Image selectedPopUp;
    [SerializeField] private TextMeshProUGUI selectedItemName;
    [SerializeField] private TextMeshProUGUI selectedItemDescription;
    [SerializeField] private TextMeshProUGUI selectedItemStatName;
    [SerializeField] private TextMeshProUGUI selectedItemStatValue;

    [SerializeField] private ItemSO selectedItem;
    [SerializeField] private int selectedItemIndex;

    [SerializeField] private Character player;

    private void Start()
    {
        // Player�� null���� Ȯ��
        if (GameManager.Instance.Player != null)
        {
            player = GameManager.Instance.Player;
        }
        else
        {
            Debug.LogError("Player ��ü�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
        }

        exitButton.onClick.AddListener(CloseInventory);
        addButton.onClick.AddListener(AddContentSize);
        equipButton.onClick.AddListener(EquipSelectedItem);
    }

    public void OpenInventory()
    {
        gameObject.SetActive(true);
    }

    public void CloseInventory()
    {
        if (selectedPopUp.gameObject.activeSelf)
        {
            selectedPopUp.gameObject.SetActive(false);
            selectedItem = null;
            selectedItemIndex = -1;
        }
        else
        {
            gameObject.SetActive(false);
        }  
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
        uiSlot.inventory = this;
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

    public void SetCharacterInfo(Character player)
    {
        inventory = player.inventory;
    }


    public void InteractionSlot(int index)
    {
        if (uiSlots[index].item == null) return;

        selectedPopUp.gameObject.SetActive(true);
        selectedItem = uiSlots[index].item;
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.itemName;
        selectedItemDescription.text = selectedItem.itemDescription;
        selectedItemStatName.text = selectedItem.itemType.ToString();
        selectedItemStatValue.text = selectedItem.itemValue.ToString();
    }

    public void EquipSelectedItem()
    {
        if (selectedItem != null)
        {
            if (selectedItem == null)
            {
                Debug.LogError("���õ� �������� �����ϴ�!");
                return;
            }

            if (player == null)
            {
                Debug.LogError("Player ��ü�� null�Դϴ�!");
                return;
            }

            player.EquipItem(selectedItem);  // Character�� EquipItem ȣ��
            UpdateStatDisplay();
            Debug.Log($"{selectedItem.itemName} ������.");
        }
    }

    public void UnEquipSelectedItem()
    {
        if (selectedItem != null)
        {
            player.UnEquipItem(selectedItem);  // Character�� UnEquipItem ȣ��
            Debug.Log($"{selectedItem.itemName} ������.");
        }
    }

    public void UpdateStatDisplay()
    {
        UIManager.Instance.UIStatus.SetCharacterInfo(player);
    }
}
