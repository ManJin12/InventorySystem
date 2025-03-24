using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [Header("Player Info")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Slider experienceValue;
    [SerializeField] private TextMeshProUGUI experienceText;
    [SerializeField] private int MaxExperience = 10;

    [Header("Gold")]
    [SerializeField] private TextMeshProUGUI goldText;

    [Header("Buttons")]
    [SerializeField] private Button statusButton;
    [SerializeField] private Button InventoryButton;

    private void Start()
    {
        statusButton.onClick.AddListener(OpenStatus);
        InventoryButton.onClick.AddListener(OpenInventory);
    }

    public void OpenMainMenu()
    {
        gameObject.SetActive(true);
    }


    public void OpenStatus()
    {
        UIManager.Instance.UIStatus.OpenStatus();
    }

    public void OpenInventory()
    {
        UIManager.Instance.Inventory.OpenInventory();
    }

    public void SetCharacterInfo(Character player)
    {
        nameText.text = $"{player.CharacterName}";
        levelText.text = $"{player.Level}";
        experienceValue.value = Mathf.Clamp01((float)player.CurrentExperience / MaxExperience);
        experienceText.text = $"{player.CurrentExperience}/{MaxExperience}";

        goldText.text = $"{player.Gold}";
    }
}
