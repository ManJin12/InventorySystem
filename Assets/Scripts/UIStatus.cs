using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button exitButton;

    [SerializeField] private TextMeshProUGUI DamageText;
    [SerializeField] private TextMeshProUGUI DefenseText;
    [SerializeField] private TextMeshProUGUI HealthTextl;
    [SerializeField] private TextMeshProUGUI CriticalText;

    private void Start()
    {
        exitButton.onClick.AddListener(CloseStatus);
    }

    public void OpenStatus()
    {
        gameObject.SetActive(true);
    }

    public void CloseStatus()
    {
        gameObject.SetActive(false);
    }

    public void SetCharacterInfo(Character player)
    {
        DamageText.text = $"{player.Damage}";
        DefenseText.text = $"{player.Defense}";
        HealthTextl.text = $"{player.Health}";
        CriticalText.text = $"{player.Critical}";
    }
}
