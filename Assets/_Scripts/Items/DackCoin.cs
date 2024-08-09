using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DackCoin : MonoBehaviour
{
    BossFinalHealth bossHP;
    UIManager uiManager;

    private void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponentInParent<UIManager>();
        bossHP = GameObject.FindGameObjectWithTag("Boss").GetComponentInParent<BossFinalHealth>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bossHP.curHP <= 0)
        {
            if(collision.gameObject.tag == "Player")
            {
                uiManager.NextLevel();
            }
        }
    }
}
