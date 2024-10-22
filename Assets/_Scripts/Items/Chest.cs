using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Animator chestAnimator;
    public GameObject coinPrefab;
    public Transform coinSpawnPoint;

    private void Start()
    {
        chestAnimator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OpenChest();
            Destroy(gameObject, 1.5f);
        }
        
    }

    void OpenChest()
    {      

        chestAnimator.SetBool("isOpen", true);

        SpawnCoins();
    }

    void SpawnCoins()
    {

        int coinCount = 1;

        for (int i = 0; i < coinCount; i++)
        {

            Vector3 spawnPosition = coinSpawnPoint.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
            GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);


            Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float forceX = Random.Range(-1f, 1f);  // Luc truc X
                float forceY = Random.Range(1f, 3f);   // Luc truc Y
                rb.AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);
            }
        }
    }
    
}
