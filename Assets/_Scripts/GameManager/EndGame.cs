using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float top;


    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        if (transform.position.y >= top)
        {
            speed = 0;
            StartCoroutine(TheEnd());
        }
    }

    IEnumerator TheEnd()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }
}
