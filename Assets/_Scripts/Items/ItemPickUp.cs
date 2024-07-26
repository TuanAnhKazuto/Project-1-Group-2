using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemUI itemUI;

    private void PickUp()
    {

        //Desstoy
        Destroy(this.gameObject);

        //Add inventory
        InventoryManager.Instance.Add(itemUI);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PickUp();
        }
    }
}
