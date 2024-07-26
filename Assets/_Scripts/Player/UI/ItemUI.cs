using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "inventory/item")]
public class ItemUI : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public Sprite image;


}
