using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PotalOut : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Vector2 scale = transform.localScale;

            while (scale.y >= 0 && scale.x >= 0)
            {
                StartCoroutine(TimeDelay());
            }
        }
    }

    IEnumerator TimeDelay()
    {
        Vector2 scale = transform.localScale;

        scale.x -= 0.1f;
        scale.y -= 0.1f;
        scale = transform.localScale;
        yield return new WaitForSeconds(0.2f);
    }
}
