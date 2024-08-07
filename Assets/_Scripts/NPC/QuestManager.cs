using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class QuestManager : MonoBehaviour
{
    TheGhost theGhost;
    public List<Quest> activeQuests; // Danh sách nhiệm vụ đang hoạt động
    [HideInInspector] public bool isQuestDone = false;
    void Start()
    {
        theGhost = GetComponent<TheGhost>();
        activeQuests = new List<Quest>();
    }

    // Update is called once per frame
    void Update()
    {
        if(theGhost.coin >= 5)
        {
            isQuestDone = true;
            Debug.Log("xong nv");
        }
    }

    public void AddQuest(Quest quest)
    {
        activeQuests.Add(quest);
        Debug.Log("Quest added: " + quest.questName);
    }

    public void CompleteQuest(Quest quest)
    {
        quest.isCompleted = true;
        Debug.Log("Quest completed: " + quest.questName);
    }
}

