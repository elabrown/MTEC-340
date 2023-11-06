using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCream : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D col;

    private SpriteRenderer spriteRenderer;
    public bool isLanded = false;
    public IceCream scoopAbove; // The scoop directly above this one
    public IceCream scoopBelow; // The scoop directly below this one

    private GameManager gameManager;
    public AudioSource audioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        
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

        if (spriteRenderer != null)
        {
            AssignRandomSprite();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        audioSource.Play();
        if (IsLayerMatch(other, "Player"))
        {
            Debug.Log("Triggered by Player");
            PlayerCollision();
            gameManager.IncreaseScore();
        }
        else if (IsLayerMatch(other, "Cone"))
        {
            Debug.Log("Landed on Cone");
            Land(null); // No ice cream on top of a cone
        }
        else if (IsLayerMatch(other, "Ice Cream"))
        {
            IceCream otherIceCream = other.GetComponent<IceCream>();
            if (otherIceCream != null && otherIceCream.isLanded)
            {
                Debug.Log("Landed on Ice Cream");
                Land(otherIceCream);
            }
        }
    }

    public void Land(IceCream onTopOf)
    {
        isLanded = true;
        StopFalling();

        if (onTopOf != null)
        {
            scoopBelow = onTopOf;
            onTopOf.scoopAbove = this;
        }
    }

    public void PlayerCollision()
    {
        if (!isLanded && scoopBelow == null)
        {
            PropagateFall();
        }
    }

    public void PropagateFall()
    {
        StartFalling();
        if (scoopAbove != null)
        {
            scoopAbove.PropagateFall();
        }
    }

    private bool IsLayerMatch(Collider2D other, string layerName)
    {
        return other.gameObject.layer == LayerMask.NameToLayer(layerName);
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
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Static;
            rb.gravityScale = 0;
        }
    }

    void AssignRandomSprite()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("IceCreamSprites");

        if (sprites.Length > 0)
        {
            int randomIndex = Random.Range(0, sprites.Length);
            spriteRenderer.sprite = sprites[randomIndex];
        }
        else
        {
            Debug.LogError("No sprites found in 'IceCreamSprites' folder!");
        }
    }
}
