using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button exitButton;
    [SerializeField] private Button addButton;
    [SerializeField] private ScrollRect scrollRect;
    private int currentSlotCount = 0;
    [SerializeField] private int addcontentCount;
    [SerializeField] private GameObject itemSlot;

    private void Start()
    {
        exitButton.onClick.AddListener(CloseInventory);
        addButton.onClick.AddListener(AddContentSize);
    }

    public void OpenInventory()
    {
        gameObject.SetActive(true);
    }
    
    public void CloseInventory()
    {
        gameObject.SetActive(false);
    }

    public void AddContentSize()
    {
        GameObject slot = Instantiate(itemSlot, scrollRect.content);
        currentSlotCount++;

        if (addcontentCount < currentSlotCount)
        {
            currentSlotCount = 1;
            scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, scrollRect.content.sizeDelta.y + 120);
        } 
    }
}
