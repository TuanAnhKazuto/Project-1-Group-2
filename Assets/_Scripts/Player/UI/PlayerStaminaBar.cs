using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaBar : MonoBehaviour
{
    public Slider staminaBar;
    private float maxStamina = 100f;
    [HideInInspector] public float curStamina;
    private float countReturn;
    TheGhost player;
    private void Start()
    {
        curStamina = maxStamina;
        player = GetComponent<TheGhost>();
    }

    private void Update()
    {
        ReturnStamina();
    }

    private void ReturnStamina()
    {
        
        if (curStamina < maxStamina)
        {
            curStamina += countReturn * Time.deltaTime;
            staminaBar.value = curStamina;
        }

        if(player.isRunning)
        {
            countReturn = 1f;
        }
        else if(player.isAttacking || player.isDashing)
        {
            countReturn = 0f;
        }
        else if(!player.isAttacking || !player.isRunning)
        {
            countReturn = 2f;
        }   
    }

    public void UpdateStaminaBar(float sub)
    {
        curStamina -= sub;
        staminaBar.value = curStamina;
    }

}
