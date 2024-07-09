using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhamVi : MonoBehaviour
{
    public float attackRange = 1.5f; // Phạm vi chém
    public Transform Enemy; // Đối tượng quái vật

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) // Nhấn phím Space để chém
        {
            float distanceToEnemy = Vector2.Distance(transform.position, Enemy.position);

            if (distanceToEnemy <= attackRange)
            {
                Attack();
            }
            else
            {
                Debug.Log("Enemy is too far to attack.");
            }
        }
  
    }

    void Attack()
    {
        Debug.Log("Attacked the enemy!");
        // Thêm mã thực hiện hành động chém quái vật tại đây, ví dụ: giảm HP của quái vật.
    }
}

