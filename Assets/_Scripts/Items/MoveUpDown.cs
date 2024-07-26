using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    private float moveSpped = 0.1f;
    int moveDisetion = 1;
    private float topMove;

    private void Start()
    {
        topMove = transform.position.y;
    }
    private void Update()
    {

        transform.Translate(Vector2.up * moveSpped * moveDisetion * Time.deltaTime);

        if (transform.position.y >= topMove)
        {
            moveDisetion = -1;
        }
        if (transform.position.y <= topMove - 0.1f)
        {
            moveDisetion = 1;
        }
    }
}
