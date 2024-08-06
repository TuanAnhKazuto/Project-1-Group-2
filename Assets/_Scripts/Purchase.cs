using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static UnityEditor.Progress;

public class ShopManager : MonoBehaviour
{
    public List<Item> itemsForSale;
    public GameObject itemPrefab;
    public Transform itemListContent;

    

    void Start()
    {
        PopulateShop();
    }

    void PopulateShop()
    {
        foreach (var item in itemsForSale)
        {
            GameObject newItem = Instantiate(itemPrefab, itemListContent);
            newItem.transform.Find("NameText").GetComponent<Text>().text = item.itemName;
            newItem.transform.Find("PriceText").GetComponent<Text>().text = item.itemPrice.ToString();
            newItem.transform.Find("ItemImage").GetComponent<Image>().sprite = item.itemImage;
            newItem.transform.Find("BuyButton").GetComponent<Button>().onClick.AddListener(() => BuyItem(item));
        }
    }

    void BuyItem(Item item)
    {
        Debug.Log("Mua sản phẩm: " + item.itemName + " với giá " + item.itemPrice);
       
    }
}
public class Item
{
    public string itemName;
    public int itemPrice;
    public Sprite itemImage;
}
