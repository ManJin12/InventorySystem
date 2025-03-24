using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Weapon,
    Armor,
    Consumable
}

public enum StatType
{
    None,
    Health,
    Damage,
    Defense,
    Critical,
    AllStat
}

[CreateAssetMenu(fileName = "Item", menuName = "ItemSO")]
public class ItemSO : ScriptableObject
{
    public string itemName; // 이름
    public string itemDescription; // 설명
    public Sprite itemImage; // 아이콘
    public Sprite slotImage; // 슬롯 이미지
    public ItemType itemType; // 아이템 타입

    public StatType statType; // 능력치 타입
    public int itemValue; // 증가되는 능력치

    public bool canEquip; // 장착 가능 여부
    public bool isEquipped; // 장착 여부
}
