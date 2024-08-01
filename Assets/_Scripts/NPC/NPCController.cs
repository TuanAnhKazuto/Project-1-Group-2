using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCController : MonoBehaviour
{
        public string storyText = "Đây là câu chuyện khi bạn va chạm với NPC.";
        public GameObject storyUI;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                // Hiển thị câu chuyện khi player va chạm với NPC
                storyUI.GetComponent<TextMeshProUGUI>().text = storyText;
                storyUI.SetActive(true);
            }
        }
 }
