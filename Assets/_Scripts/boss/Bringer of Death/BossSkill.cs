using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    TheGhost player;

    void Start()
    {
        
        Destroy(gameObject, 1.2f); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
