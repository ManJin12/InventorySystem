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
            Destroy(gameObject); // �ߺ��� GameManager�� ������ �ʵ��� ����
        }
    }


    private void Start()
    {
        SetData();
    }

    public void SetData()
    {
        Player = new Character(characterData); // ScriptableObject ������� ĳ���� ����

        UIManager.Instance.MainMenu.SetCharacterInfo(Player);
        UIManager.Instance.UIStatus.SetCharacterInfo(Player);
    }
}
