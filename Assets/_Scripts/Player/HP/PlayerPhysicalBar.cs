using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPhysicalBar : MonoBehaviour
{
    public Slider physicalBar;
    private float maxPhysical = 100f;
    [HideInInspector] public float curPhysical;

    private void Start()
    {
        curPhysical = maxPhysical;
    }



    public void UpdatePhysical(float sub)
    {
        curPhysical -= sub;
        physicalBar.value = curPhysical;
    }

}
