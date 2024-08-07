using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;
    //[SerializeField] private AudioSource victoryAudioSource;
    //[SerializeField] private AudioSource mainSound;

    private void Start()
    {
        Time.timeScale = 1;
        victoryPanel.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        QuestManager questManager = collision.GetComponentInParent<QuestManager>();
        if(collision.gameObject.tag == "Player")
        {
            if (questManager.isQuestDone)
            {
                Time.timeScale = 0;
                //victoryAudioSource.Play();
                //mainSound.Stop();
                victoryPanel.SetActive(true);
            }
            else
            {
                Debug.Log("chua xong nv");
            }
        }
    }
}
