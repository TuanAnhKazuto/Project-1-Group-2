using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1.2f); 
    }
}
