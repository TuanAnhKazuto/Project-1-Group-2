using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fillBar;
    [SerializeField] private GameObject firstLife;
    [SerializeField] private GameObject finalLife;
    public ParticleSystem subLifeEffect1;
    public ParticleSystem subLifeEffect2;


    public void UpdateBar(int maxHP, int curHP)
    {
        fillBar.fillAmount = (float)curHP / (float)maxHP;
    }

    public void UpdateLife(int maxLife, int curLife)
    {
        if(curLife == 2)
        {
            Destroy(firstLife);
            subLifeEffect1.Play();
        }
        if(curLife == 1)
        {
            Destroy(finalLife);
            subLifeEffect2.Play();
        }
    }
}
