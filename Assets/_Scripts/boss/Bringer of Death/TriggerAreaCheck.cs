using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaCheck : MonoBehaviour
{
    BossBehaviour bossParent;
    private void Awake()
    {
        bossParent = GetComponentInParent<BossBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            bossParent.target = collision.transform;
            bossParent.inRange = true;
            bossParent.hotZone.SetActive(true);
        }
    }
}
