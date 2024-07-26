using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    Vector2 checkPointPos;
    Rigidbody2D playerRb;
    private void Start()
    {
        checkPointPos = transform.position;
        playerRb = GetComponentInParent<Rigidbody2D>();
    }

    public void UpDateCheckpoint(Vector2 pos)
    {
        checkPointPos = pos;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        StartCoroutine(RespawnIE());
    }

    IEnumerator RespawnIE()
    {
        playerRb.velocity = new Vector2(0, 0);
        playerRb.simulated = false;
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(0.3f);
        transform.position = checkPointPos;
        transform.localScale = new Vector3(1, 1, 1);
        playerRb.simulated = true;
    }

}
