using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCream : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D col;

    private SpriteRenderer spriteRender;

    public bool isLanded = false;

    private GameManager gameManager;


    private void Awake()
    {
        // Get components from the object
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        spriteRender = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();

        // Null checks and initial setup
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        if (rb != null)
        {
            rb.gravityScale = 0;
            rb.bodyType = RigidbodyType2D.Static;
        }

        if (col != null)
        {
            col.isTrigger = true;
        }

        if (spriteRender != null)
        {
            spriteRender.color = Random.ColorHSV();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsLayerMatch(other, "Player"))
        {
            Debug.Log("Triggered by Player");
            StartFalling();
            gameManager.IncreaseScore();
        }
        else if (IsLayerMatch(other, "Pallet"))
        {
            Debug.Log("Landed on Pallet");
            StopFalling();
        }
        else if (IsLayerMatch(other, "Ice Cream"))
        {
            HandleIceCreamCollision(other);
        }
    }

    private void HandleIceCreamCollision(Collider2D other)
    {
        IceCream aboveIceCream = other.gameObject.GetComponent<IceCream>();

        if (aboveIceCream != null)
        {
            if (!aboveIceCream.isLanded)
            {
                aboveIceCream.StartFalling();
            }
            else
            {
                StopFalling();
            }
        }
    }

    private bool IsLayerMatch(Collider2D other, string layerName)
    {
        return other.gameObject != null && other.gameObject.layer == LayerMask.NameToLayer(layerName);
    }

    public void StartFalling()
    {
        isLanded = false;
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 1;
        }
    }

    public void StopFalling()
    {
        isLanded = true;
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Static;
            rb.gravityScale = 0;
        }
    }
}
