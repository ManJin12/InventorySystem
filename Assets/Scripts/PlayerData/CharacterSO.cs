using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character")]
public class CharacterSO : ScriptableObject
{
    [Header("Character Info")]
    public string characterName;
    public int level;
    public int exp;
    public int gold;

    [Header("Character Status")]
    public int damage;
    public int defense;
    public int health;
    public int critical;
}
