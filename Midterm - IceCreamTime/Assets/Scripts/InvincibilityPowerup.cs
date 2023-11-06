using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityPowerup : MonoBehaviour
{
    public GameObject player;
    public AudioSource gameManagerAudioSource;
    public AudioClip powerupSound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != null && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (player != null)
            {
                // Get the MovementScript component and call the ActivatePowerup method
                MovementScript movementScript = player.GetComponent<MovementScript>();
                if (movementScript != null)
                {
                    movementScript.ActivatePowerup();
                }
                else
                {
                    Debug.LogError("MovementScript not found on the player object!");
                }
            }
            else
            {
                Debug.LogError("Player object not found!");
            }
            gameManagerAudioSource.clip = powerupSound;
            gameManagerAudioSource.Play();
            Destroy(this.gameObject);
        }
    }
}
