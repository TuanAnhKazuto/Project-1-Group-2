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
            if (animatorkey != null)
            {
                animatorkey.SetTrigger("Key");
            }

            StartCoroutine(HideKeyAndUpdateDoor());
            if (boxColliderDoor != null)
            {
                boxColliderDoor.enabled = false; 
            }
        }
    }

    IEnumerator HideKeyAndUpdateDoor()
    {
        // Đợi thời gian của hoạt hình khóa trước khi tắt khóa
        yield return new WaitForSeconds(animatorkey.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);

        // Đợi 2 giây sau khi khóa biến mất trước khi tắt collider của cửa và thay đổi hoạt hình
        yield return new WaitForSeconds(2f);

        if (boxColliderDoor != null)
        {
            boxColliderDoor.enabled = false;
        }

        if (animatordoor != null)
        {
            animatordoor.SetTrigger("IdleDoor");
        }
    }
}
