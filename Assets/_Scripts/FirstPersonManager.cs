using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonManager : MonoBehaviour {
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor to the center of the screen
        Cursor.visible = false; // Hides the cursor
    }
}
