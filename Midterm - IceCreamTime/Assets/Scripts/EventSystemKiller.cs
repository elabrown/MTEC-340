using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class EventSystemKiller : MonoBehaviour
{
    void Awake()
    {
        EventSystem[] eventSystems = FindObjectsOfType<EventSystem>();
        if (eventSystems.Length > 1)
        {
            Destroy(gameObject); // Assuming this script is attached to the EventSystem
        }

        DontDestroyOnLoad(gameObject);
    }

    
}