﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lamp : MonoBehaviour
{

    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();

         
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Samurai")
        {
            animator.SetBool("isCheckPoint", true);
            PlayerPrefs.SetFloat("PlayerPosX", transform.position.x);
            PlayerPrefs.SetFloat("PlayerPosY", transform.position.y);
            Debug.Log("Saved checkpoint: " + transform.position);
        }

        if (other.CompareTag("Samurai"))
        {
            LoadLevel();
        }
    }

    public string Level;

    public void LoadLevel()
    {
        SceneManager.LoadScene(Level);
    }



    

}
