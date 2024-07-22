using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPhysicalBar : MonoBehaviour
{
    public Slider physicalBar;
    private float maxPhysical = 100f;
    [HideInInspector] public float curPhysical;
    private float countReturn = 0.5f;
    private void Start()
    {
        curPhysical = maxPhysical;

    }

    private void Update()
    {
        if(curPhysical < maxPhysical)
        {
            curPhysical += countReturn * Time.deltaTime;
            physicalBar.value = curPhysical;
        }
    }

    public void UpdatePhysical(float sub)
    {
        curPhysical -= sub;
        physicalBar.value = curPhysical;
    }

}
