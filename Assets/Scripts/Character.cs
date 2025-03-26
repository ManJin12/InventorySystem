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
    public Dictionary<ItemType, ItemSO> equippedItems;  // 장착된 아이템을 ItemType별로 저장

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
            Debug.LogWarning("장착할 수 없는 아이템입니다.");
            return;
        }

        // 같은 타입의 아이템이 이미 장착되어 있으면 해제
        if (equippedItems.TryGetValue(itemSO.itemType, out ItemSO equippedItem))
        {
            UnEquipItem(equippedItem);
        }

        // 장착 처리
        if (statModifiers.TryGetValue(itemSO.statType, out var applyStat))
        {
            applyStat(itemSO.itemValue);
            equippedItems[itemSO.itemType] = itemSO;  // 아이템을 장착 목록에 추가
            Debug.Log($"{itemSO.itemType} 장착됨: {itemSO.statType} +{itemSO.itemValue}");
        }
        else
        {
            Debug.LogWarning("적용할 수 없는 스탯 타입");
        }
    }

    public void UnEquipItem(ItemSO itemSO)
    {
        if (!equippedItems.ContainsValue(itemSO))
        {
            Debug.LogWarning("장착 중인 아이템이 아닙니다.");
            return;
        }

        // 능력치 되돌리기
        if (statModifiers.TryGetValue(itemSO.statType, out var applyStat))
        {
            applyStat(-itemSO.itemValue);
            equippedItems.Remove(itemSO.itemType);
            Debug.Log($"{itemSO.itemType} 해제됨: {itemSO.statType} -{itemSO.itemValue}");
        }
        else
        {
            Debug.LogWarning("적용할 수 없는 스탯 타입");
        }
    }

}
