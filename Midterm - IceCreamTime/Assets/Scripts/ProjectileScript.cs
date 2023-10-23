using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Vector2 direction;
    private GameManager gameManager;
    private Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        col = GetComponent<Collider2D>();

        //if the projectile is spawned on the left, it moves right, and
        // vice versa
        if (transform.position.x < 0) { direction = Vector2.right; }
        else if (transform.position.x > 0) { direction = Vector2.left; }
    }

    private void Update()
    {
        // Move the projectile every frame
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    public void SetDirection(Vector2 newDirection)
    {
        // Normalize to ensure it only affects the direction, not the speed
        direction = newDirection.normalized;
    }

    private void OnBecameInvisible()
    {
        // Destroy the projectile once it's off-screen
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != null && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            gameManager.LevelFailed();
        }
    }
}
