using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }


    public Character Player { get; private set; }

    [SerializeField] private CharacterSO characterData;
    public List<ItemSO> itemSOs;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject); // 중복된 GameManager가 생기지 않도록 방지
        }
    }


    private void Start()
    {
        SetData();
    }

    public void SetData()
    {
        Player = new Character(characterData); // ScriptableObject 기반으로 캐릭터 생성

        UIManager.Instance.MainMenu.SetCharacterInfo(Player);
        UIManager.Instance.UIStatus.SetCharacterInfo(Player);
    }
}
