using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class TestCamMOve : MonoBehaviour
{
    private float moveSpeed = 10f;
    [SerializeField] private float startCam;
    [SerializeField] private float stopCam;

    void Update()
    {
        if(transform.position.x == startCam)
        {
            if (Input.GetKey(KeyCode.P))
            {
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            }
        }
        if(transform.position.x >= stopCam)
        {
            StartCoroutine(moveBack());
        }
    }

    IEnumerator moveBack()
    {
        yield return new WaitForSeconds(2f);
            transform.Translate(Vector2.right * -moveSpeed * Time.deltaTime);
    }

}
