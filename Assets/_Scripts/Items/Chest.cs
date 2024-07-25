using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Animator chestAnimator; 
    private bool isOpen = false;   

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Player") && !isOpen)
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        if (chestAnimator != null)
        {
            chestAnimator.SetTrigger("Open"); 
            isOpen = true; 
        }
    }
}
