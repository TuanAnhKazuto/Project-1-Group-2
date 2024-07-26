using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    
    public List<ItemUI> items = new();

    public Transform itemHolder;
    public GameObject itemPrefab;
    private void Awake()
    {
        if (Instance != null || Instance != this)
        {
            Destroy(Instance);
        }

        Instance = this;
    }

    public void Add(ItemUI item)
    {
        items.Add(item);
    }

    public void DisplayInventory()
    {
        foreach (ItemUI item in items) 
        {
            GameObject obj = Instantiate(itemPrefab, itemHolder);

            var itemValue = obj.transform.Find("ItemValue").GetComponent<TextMeshProUGUI>();
            var itemImage = obj.transform.Find("ItemImage").GetComponent<Image>();
        }
    }


}
