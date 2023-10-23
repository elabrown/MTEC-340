using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerKiller : MonoBehaviour
{
    void Awake()
    {
        AudioListener[] listeners = FindObjectsOfType<AudioListener>();
        if (listeners.Length > 1)
        {
            Destroy(gameObject); // Assuming this script is attached to the object with the AudioListener
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}