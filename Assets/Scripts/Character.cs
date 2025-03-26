using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public string CharacterName { get; set; }
    public int Level { get; private set; }
    public int CurrentExperience { get; private set; }
    public int Gold { get; private set; }

    public int Damage { get; private set; }
    public int Defense { get; private set; }
    public int Health { get; private set; }
    public int Critical { get; private set; }

    public List<ItemSO> inventory;
    public Dictionary<ItemType, ItemSO> equippedItems;  // ������ �������� ItemType���� ����

    private Dictionary<StatType, Action<int>> statModifiers;

    public Character(CharacterSO characterData)
    {
        CharacterName = characterData.characterName;
        Level = characterData.level;
        CurrentExperience = characterData.exp;
        Damage = characterData.damage;
        Defense = characterData.defense;
        Health = characterData.health;
        Critical = characterData.critical;
        inventory = new List<ItemSO>();
        equippedItems = new Dictionary<ItemType, ItemSO>();

        statModifiers = new Dictionary<StatType, Action<int>>
        {
            { StatType.Damage, (value) => Damage += value },
            { StatType.Critical, (value) => Critical += value },
            { StatType.Defense, (value) => Defense += value },
            { StatType.Health, (value) => Health += value }
        };
    }

    public void AddItem(ItemSO itemSO)
    {
        inventory.Add(itemSO);
    }

    public void EquipItem(ItemSO itemSO)
    {
        if (!itemSO.canEquip)
        {
            Debug.LogWarning("������ �� ���� �������Դϴ�.");
            return;
        }

        // ���� Ÿ���� �������� �̹� �����Ǿ� ������ ����
        if (equippedItems.TryGetValue(itemSO.itemType, out ItemSO equippedItem))
        {
            UnEquipItem(equippedItem);
        }

        // ���� ó��
        if (statModifiers.TryGetValue(itemSO.statType, out var applyStat))
        {
            applyStat(itemSO.itemValue);
            equippedItems[itemSO.itemType] = itemSO;  // �������� ���� ��Ͽ� �߰�
            Debug.Log($"{itemSO.itemType} ������: {itemSO.statType} +{itemSO.itemValue}");
        }
        else
        {
            Debug.LogWarning("������ �� ���� ���� Ÿ��");
        }
    }

    public void UnEquipItem(ItemSO itemSO)
    {
        if (!equippedItems.ContainsValue(itemSO))
        {
            Debug.LogWarning("���� ���� �������� �ƴմϴ�.");
            return;
        }

        // �ɷ�ġ �ǵ�����
        if (statModifiers.TryGetValue(itemSO.statType, out var applyStat))
        {
            applyStat(-itemSO.itemValue);
            equippedItems.Remove(itemSO.itemType);
            Debug.Log($"{itemSO.itemType} ������: {itemSO.statType} -{itemSO.itemValue}");
        }
        else
        {
            Debug.LogWarning("������ �� ���� ���� Ÿ��");
        }
    }

}
