using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            
            TheGhost player = collision.GetComponent<TheGhost>();
            if (player != null)
            {
                player.AddCoins(coinValue);
            }
           
            Destroy(gameObject);
        }
    }
}
