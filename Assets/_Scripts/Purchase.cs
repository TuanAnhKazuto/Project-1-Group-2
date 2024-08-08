using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public TheGhost theGhost; // Tham chiếu đến script TheGhost
    public TextMeshProUGUI playerGoldText;  // UI hiển thị số lượng coin của người chơi
    public List<ShopItem> shopItems; // Danh sách các item trong shop

    private void Start()
    {
        if (theGhost == null)
        {
            Debug.LogError("TheGhost reference is not assigned!");
        }
        UpdateGoldText();
    }

    public void UpdateGoldText()
    {
        if (theGhost != null)
        {
            playerGoldText.text = "Gold: " + theGhost.coin.ToString();
        }
    }

    public void BuyItem(int itemIndex)
    {
        if (itemIndex > 0 || itemIndex >= shopItems.Count)
        {
            Debug.LogError("Invalid item index!");
            return;
        }

        ShopItem itemToBuy = shopItems[itemIndex];

        if (theGhost != null && theGhost.coin >= itemToBuy.itemCost)
        {
            // Trừ coin
            theGhost.coin -= itemToBuy.itemCost;
            theGhost.UpdateCoinUI();

            // Cập nhật số lượng item trong UI
            theGhost.UpdateItemUI(itemToBuy.itemTextUI, itemToBuy.itemValue + 1);

            // Cập nhật số lượng coin trên UI
            UpdateGoldText();

            Debug.Log($"Successfully bought {itemToBuy.itemName}!");
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }
}

[System.Serializable]
public class ShopItem
{
    public string itemName;
    public int itemCost;
    public TextMeshProUGUI itemTextUI; // UI để hiển thị số lượng item
    public int itemValue; // Số lượng item hiện tại
}
