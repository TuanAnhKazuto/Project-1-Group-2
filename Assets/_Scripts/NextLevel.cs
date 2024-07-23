using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    public Canvas myCanvas; 
    public Button myButton; 

    public string Level;

   
    void Start()
    {
        
        if (myCanvas != null)
        {
            myCanvas.enabled = false; 
        }

        if (myButton != null)
        {
            myButton.onClick.AddListener(LoadLevel); 
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowCanvas();
        }
    }

    
    void Update()
    {

    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(Level);
        
    }

    void ShowCanvas()
    {
        if (myCanvas != null)
        {
            myCanvas.enabled = true;
           
        }
    }   
}
