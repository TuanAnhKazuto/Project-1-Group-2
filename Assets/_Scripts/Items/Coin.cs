using UnityEngine;

public class Coin : MonoBehaviour
{
    public int points = 1; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TheGhost player = collision.GetComponent<TheGhost>();
        if (player != null)
        {
            
            Destroy(gameObject);
        }
    }
}
