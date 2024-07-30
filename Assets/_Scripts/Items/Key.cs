using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    public GameObject door; 
    private BoxCollider2D boxColliderDoor; 
    private Animator animatorkey; 
    private Animator animatordoor; 

    void Start()
    {
        // Lấy Animator
        animatorkey = GetComponent<Animator>();

        if (door != null)
        {
            boxColliderDoor = door.GetComponent<BoxCollider2D>();
            animatordoor = door.GetComponent<Animator>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //// Chạy animation
            //if (animatorkey != null)
            //{
            //    animatorkey.SetTrigger("Rotate");
            //}

            //// Ẩn đối tượng sau khi chạy animation
            //StartCoroutine(HideObjectA());

            //if (animatordoor != null)
            //{
            //    animatordoor.SetTrigger("MoveUp");
            //}

            // Vô hiệu hóa BoxCollider2D 
            if (boxColliderDoor != null)
            {
                StartCoroutine(DisableBoxCollider());
            }
        }
    }

    IEnumerator HideObjectA()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    IEnumerator DisableBoxCollider()
    {
        yield return new WaitForSeconds(2f);
        boxColliderDoor.enabled = false;
    }
}