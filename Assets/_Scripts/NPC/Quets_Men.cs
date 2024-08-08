using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quets_Men : MonoBehaviour 
{
    public GameObject questPanel_Men; // Tham chiếu đến Panel nhiệm vụ
    private bool isQuestPanelActive = false;

    void Update()
    {
        // Kiểm tra khi người chơi bấm phím N
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleQuestPanel();
        }
    }
    private void Start()
    {
        questPanel_Men.SetActive(false);
    }

    void ToggleQuestPanel()
    {
        // Thay đổi trạng thái của Panel
        isQuestPanelActive = !isQuestPanelActive;
        questPanel_Men.SetActive(isQuestPanelActive);
    }
}

