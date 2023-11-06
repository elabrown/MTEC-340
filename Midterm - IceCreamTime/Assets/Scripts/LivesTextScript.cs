using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesTextScript : MonoBehaviour
{
    private Scener scener;
    // Start is called before the first frame update
    void Start()
    {
        scener = FindObjectOfType<Scener>();
        scener.AssignLivesText();
    }
}
