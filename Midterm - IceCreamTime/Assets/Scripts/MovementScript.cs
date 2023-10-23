using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;

    private Collider2D[] overlaps = new Collider2D[4];
    private Vector2 direction;

    private bool grounded;
    private bool onladder;

    public float moveSpeed = 3f;

    private int groundLayer;
    private int ladderLayer;

    private GameManager gameManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        // Cache the layer indices
        groundLayer = LayerMask.NameToLayer("Ground");
        ladderLayer = LayerMask.NameToLayer("Ladder");

        // Cache the GameManager reference
        gameManager = FindObjectOfType<GameManager>();

        transform.localScale = new Vector3(0.65f, 0.65f, 0.65f);
    }

    private void Update()
    {
        ReadInput();
        CheckCollision();
    }

    private void FixedUpdate()
    {
        ApplyPhysics();
    }

    private void ReadInput()
    {
        direction.x = Input.GetAxis("Horizontal") * moveSpeed;

        if (onladder)
        {
            direction.y = Input.GetAxis("Vertical") * moveSpeed;
        }
    }

    private void CheckCollision()
    {
        grounded = false;
        onladder = false;

        Vector2 size = col.bounds.size;
        size.x /= 2f;

        int amount = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0f, overlaps);

        for (int i = 0; i < amount; i++)
        {
            GameObject hit = overlaps[i].gameObject;

            if (hit.layer == groundLayer)
            {
                if (Mathf.Abs(direction.y) < 0.01f) 
                {
                    grounded = hit.transform.position.y < (transform.position.y - 0.3f);
                    Physics2D.IgnoreCollision(overlaps[i], col, !grounded);
                }
                else
                {
                    Physics2D.IgnoreCollision(overlaps[i], col);
                }
            }
            else if (hit.layer == ladderLayer)
            {
                onladder = true;
            }
        }
    }

    private void ApplyPhysics()
    {
        if (!onladder)
        {
            direction += Physics2D.gravity * Time.fixedDeltaTime;

            if (grounded)
            {
                direction.y = Mathf.Max(direction.y, -1f);
            }
        }

        // Flipping the sprite based on direction
        if (direction.x > 0f)
        {
            transform.localScale = new Vector3(0.65f, 0.65f, 0.65f);
        }
        else if (direction.x < 0f)
        {
            transform.localScale = new Vector3(-0.65f, 0.65f, 0.65f);
        }

        rb.MovePosition(rb.position + direction * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        
        //else if (collision.gameObject.CompareTag("Obstacle"))
        //{
        //    this.enabled = false;
        //    gameManager.LevelFailed();
        //}
    }
}
