using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCController : MonoBehaviour
{
      
        public GameObject storyUI;
    private void Start()
    {
        storyUI.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Hiển thị câu chuyện khi player va chạm với NPC
            
            storyUI.SetActive(true);
            Invoke("RemoveText", 1);
        }
    }

    private void RemoveText()
    {
        storyUI.SetActive(false);

    }
}
