using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class NPCCT : MonoBehaviour
{
    Animator animator;
   
    public GameObject exclamationMark; // dấu chấm than
    public GameObject questPanel;  //bản nhiệm vụ
    private bool isPlayerNearby = false;



    void Start()
    {
      
        animator = GetComponentInChildren<Animator>();
        questPanel.SetActive(false); // Ẩn bản nhiệm vụ khi bắt đầu
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("Cham", true);
            isPlayerNearby = true;
            questPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("Cham", false);
            exclamationMark.SetActive(false); //hiện lại bản chấm than khi người chơi rời xa
            isPlayerNearby = false;
            questPanel.SetActive(false); // Ẩn bản nhiệm vụ khi người chơi rời xa
        }
    }

    void Update()
    {
        // Kiểm tra nếu người chơi bấm phím "N" và đang gần NPC
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.N))
        {
            questPanel.SetActive(!questPanel.activeSelf); // Hiện hoặc ẩn bản nhiệm vụ khi bấm "N"
            exclamationMark.SetActive(false);

           
        }
    }
    
}

  
