using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    private static int lives = 3;

    private static int _score = 1000;


    [SerializeField] public GameObject bulletprefabs;
    [SerializeField] private bool _isMovingRight = true;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private LayerMask jumpableGround;
    public Transform guntransform;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float jumpForce = 4f;
    private enum MovementState { NhanVatDungYen, NhanVatChay, NhanVatNhay, NhanVatTiepDat }


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        Fire();
        UpdateAnimationState();

    }
    private void UpdateAnimationState()
    {
        MovementState state;
        if (dirX > 0f)
        {

            state = MovementState.NhanVatChay;
            sprite.flipX = false;
            _isMovingRight = true;
        }
        else if (dirX < 0f)
        {
            state = MovementState.NhanVatChay;
            sprite.flipX = true;
            _isMovingRight = false;
        }
        else
        {
            state = MovementState.NhanVatDungYen;
        }
        if (rb.velocity.y > .1f)
        {
            state = MovementState.NhanVatNhay;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.NhanVatTiepDat;
        }
        anim.SetInteger("state", (int)state);

    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Tạo viên đạn tại vị trí Gun
            var oneBullet = Instantiate(bulletprefabs, guntransform.position, Quaternion.identity);
            // cho viên đạn bay theo hướng của nhân vật

            if (_isMovingRight == true)
            {
                oneBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 0);
            }
            else
            {
                oneBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-5f, 0);
            }
            Destroy(oneBullet, 2f); //Đạn biến mất sau 2 giây
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Top")
        {
            string name = other.attachedRigidbody.name;
            Destroy(GameObject.Find(name));
        }
        if (other.gameObject.tag == "EnemieRed")
        {
            lives -= 1;
            if (lives > 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                Destroy(gameObject);
                _gameOverPanel.SetActive(true);
                // dung game 
                Time.timeScale = 0;
            }
        }
    }
    public int GetScore()
    {
        return _score;
    }

}


