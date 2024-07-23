using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
  
    // Start is called before the first frame update
    void Start()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LoadLevel();
        }  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string Level;
    public void LoadLevel()
    {
        SceneManager.LoadScene(Level);
    }

   
}
