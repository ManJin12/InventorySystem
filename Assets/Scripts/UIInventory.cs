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

        // ������ ������ ���� �������� �˻�
        if (uiSlots[index].equiped)
        {
            equipText.text = "����";
        }
        else
        {
            equipText.text = "����";
        }
    }

    public void EquipSelectedItem()
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

        // ���� ������ ������ ����
        if (equippedItemSlots.TryGetValue(selectedItem.itemType, out UISlot previousSlot))
        {
            previousSlot.equipIcon.gameObject.SetActive(false);
            previousSlot.equiped = false; // ���� ���� ���� ����
        }

        player.EquipItem(selectedItem);

        // ���Ӱ� ������ �������� ������ ã�Ƽ� equipIcon Ȱ��ȭ
        UISlot slot = uiSlots[selectedItemIndex];
        slot.equipIcon.gameObject.SetActive(true);
        slot.equiped = true; // ���� ���� ����

        // ���Ӱ� ������ ������ ����
        equippedItemSlots[selectedItem.itemType] = slot;

        // UI ������Ʈ
        equipText.text = "����";
        UpdateStatDisplay();
        Debug.Log($"{selectedItem.itemName} ������.");

    }

    public void UnEquipSelectedItem()
    {
        if (selectedItem == null)
        {
            Debug.LogError("���õ� �������� �����ϴ�!");
            return;
        }

        player.UnEquipItem(selectedItem);

        // ���� ������ �������� equipIcon�� ��Ȱ��ȭ
        if (equippedItemSlots.TryGetValue(selectedItem.itemType, out UISlot slot))
        {
            slot.equipIcon.gameObject.SetActive(false);
            slot.equiped = false; // �ش� ���� ���� ����
            equippedItemSlots.Remove(selectedItem.itemType);
        }

        // UI ������Ʈ
        equipText.text = "����";
        Debug.Log($"{selectedItem.itemName} ������.");
    }

    public void UpdateStatDisplay()
    {
        UIManager.Instance.UIStatus.SetCharacterInfo(player);
    }
}