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
    public string itemName; // �̸�
    public string itemDescription; // ����
    public Sprite itemImage; // ������
    public Sprite slotImage; // ���� �̹���
    public ItemType itemType; // ������ Ÿ��

    public StatType statType; // �ɷ�ġ Ÿ��
    public int itemValue; // �����Ǵ� �ɷ�ġ

    public bool canEquip; // ���� ���� ����
    public bool isEquipped; // ���� ����
}
