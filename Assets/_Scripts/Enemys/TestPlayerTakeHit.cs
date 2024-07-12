using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerTakeHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyATK")
        {
            Debug.Log("Aa!!!");
        }
    }
}
