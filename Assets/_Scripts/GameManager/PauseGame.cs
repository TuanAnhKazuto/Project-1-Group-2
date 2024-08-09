using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject shopPanel;
    public GameObject poolText;
    bool isPauseGame = false;

    TheGhost player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<TheGhost>();
        pausePanel.SetActive(false);
        shopPanel.SetActive(false);
        poolText.SetActive(false);
    }

    public void Update()
    {
        if (!isPauseGame)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGamePanel();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ResumeGame();
            }
        }
    }

    public void PauseGamePanel()
    {
        isPauseGame = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPauseGame = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShopPanel()
    {
        shopPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void ExitShop()
    {
        shopPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void ShopBuyHP()
    {
        if(player.coin >= 20)
        {
            player.coin -= 20;
            player.oniginiValue += 1;
            player.onigiriText.text = player.oniginiValue.ToString();
        }
        else
        {
            PoolTextOn();
        }
    }
    public void ShopBuyMP()
    {
        if(player.coin >= 20)
        {
            player.coin -= 20;
            player.sakekasuValue += 1;
            player.sakekasuText.text = player.sakekasuValue.ToString();
        }
        else
        {
            PoolTextOn();
        }
    }

    private void PoolTextOn()
    {
        poolText.SetActive(true);
        Time.timeScale = 1f;
        Invoke("PoolTextOff", 0.3f);
    }

    private void PoolTextOff()
    {
        poolText.SetActive(false);

        Time.timeScale = 0f;
    }
}
