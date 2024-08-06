using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class NPCCT : MonoBehaviour
{
     public GameObject exclamationMark; // dấu chấm than
     public GameObject questPanel;  //bản nhiệm vụ
     private bool isPlayerNearby = false;

     void Start()
     {
       exclamationMark.SetActive(false); // Ẩn dấu chấm than khi bắt đầu
       questPanel.SetActive(false); // Ẩn bản nhiệm vụ khi bắt đầu
     }

     void OnTriggerEnter(Collider other)
     {
       if (other.CompareTag("Player"))
        {
          exclamationMark.SetActive(true); 
          isPlayerNearby = true;
        }
     }

     void OnTriggerExit(Collider other)
     {
        if (other.CompareTag("Player"))
         {
           exclamationMark.SetActive(false); // Ẩn dấu chấm than khi người chơi rời xa
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
          }
      }
  }
  
