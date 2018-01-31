using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this to the Left camera to draw the VR mask
/// (the rectangular with black borders)
/// </summary>
public class DrawVrMask : MonoBehaviour
{
    public Texture2D VrMask;

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), VrMask);
    }
}
