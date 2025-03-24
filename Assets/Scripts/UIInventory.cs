using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button exitButton;

    private void Start()
    {
        exitButton.onClick.AddListener(CloseInventory);
    }

    public void OpenInventory()
    {
        gameObject.SetActive(true);
    }
    
    public void CloseInventory()
    {
        gameObject.SetActive(false);
    }
}
