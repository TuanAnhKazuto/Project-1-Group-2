using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
  
        public GameObject storyUI;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                // Toggle visibility of the story UI
                storyUI.SetActive(!storyUI.activeSelf);
            }
        }
 }
