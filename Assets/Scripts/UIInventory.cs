using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

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

    private Dictionary<ItemType, UISlot> equippedItemSlots = new Dictionary<ItemType, UISlot>();


    [SerializeField] private Character player;

    private void Start()
    {
        // Player가 null인지 확인
        if (GameManager.Instance.Player != null)
        {
            player = GameManager.Instance.Player;
        }
        else
        {
            Debug.LogError("Player 객체가 초기화되지 않았습니다.");
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
        for (int i = 0; i < uiSlots.Count; i++)
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

        // 선택한 슬롯이 장착 상태인지 검사
        if (uiSlots[index].equiped)
        {
            equipText.text = "해제";
        }
        else
        {
            equipText.text = "장착";
        }
    }

    public void EquipSelectedItem()
    {
        if (selectedItem == null)
        {
            Debug.LogError("선택된 아이템이 없습니다!");
            return;
        }

        if (player == null)
        {
            Debug.LogError("Player 객체가 null입니다!");
            return;
        }

        // 기존 장착된 아이템 해제
        if (equippedItemSlots.TryGetValue(selectedItem.itemType, out UISlot previousSlot))
        {
            previousSlot.equipIcon.gameObject.SetActive(false);
            previousSlot.equiped = false; // 기존 슬롯 장착 해제
        }

        player.EquipItem(selectedItem);

        // 새롭게 장착한 아이템의 슬롯을 찾아서 equipIcon 활성화
        UISlot slot = uiSlots[selectedItemIndex];
        slot.equipIcon.gameObject.SetActive(true);
        slot.equiped = true; // 장착 상태 저장

        // 새롭게 장착한 슬롯을 저장
        equippedItemSlots[selectedItem.itemType] = slot;

        // UI 업데이트
        equipText.text = "해제";
        UpdateStatDisplay();
        Debug.Log($"{selectedItem.itemName} 장착됨.");

    }

    public void UnEquipSelectedItem()
    {
        if (selectedItem == null)
        {
            Debug.LogError("선택된 아이템이 없습니다!");
            return;
        }

        player.UnEquipItem(selectedItem);

        // 장착 해제된 아이템의 equipIcon을 비활성화
        if (equippedItemSlots.TryGetValue(selectedItem.itemType, out UISlot slot))
        {
            slot.equipIcon.gameObject.SetActive(false);
            slot.equiped = false; // 해당 슬롯 장착 해제
            equippedItemSlots.Remove(selectedItem.itemType);
        }

        // UI 업데이트
        equipText.text = "장착";
        Debug.Log($"{selectedItem.itemName} 해제됨.");
    }

    public void UpdateStatDisplay()
    {
        UIManager.Instance.UIStatus.SetCharacterInfo(player);
    }
}