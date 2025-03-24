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

    public Character(CharacterSO characterData)
    {
        CharacterName = characterData.characterName;
        Level = characterData.level;
        CurrentExperience = characterData.exp;
        Damage = characterData.damage;
        Defense = characterData.defense;
        Health = characterData.health;
        Critical = characterData.critical;
    }
}
