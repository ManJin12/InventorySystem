using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    [SerializeField] private ItemSO itemSO;

    private void Start()
    {
        itemSO = GetComponent<ItemSO>();
    }

    public string GetInteractText()
    {
        return "Pick up";
    }
}
