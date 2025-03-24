using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    [Header("UI Object")]
    [SerializeField] private UIMainMenu mainMenu;
    [SerializeField] private UIStatus uIStatus;
    [SerializeField] private UIInventory inventory;

    public UIMainMenu MainMenu { get { return mainMenu; } }
    public UIStatus UIStatus { get { return uIStatus; } }
    public UIInventory Inventory { get { return inventory; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject); // 중복된 UIManager가 생기지 않도록 방지
        }
    }
}
