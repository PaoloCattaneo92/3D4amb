using Assets._3D4amb_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach me to the LEFT camera
/// </summary> 
public class DrawEyePatch : MonoBehaviour
{
    public Texture2D EyePatch;
    public PenaltyManager PM;

    void Awake()
    {
        while(PM==null)
        {
            PM = GameObject.Find("PenaltyManager").GetComponent<PenaltyManager>();
        }
    }

    void OnGUI()
    {
        if(PM!=null)    //skippa i frame in cui non ha ancora inizializzato PM
        {
            GUI.color = new Color(0, 0, 0, PM.PenaltyInfoNow.PenaltyEyePatch);
            if (PM.HealthyEye == Eye.LEFT)
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width / 2, Screen.height), EyePatch);
            }
            else
            {
                GUI.DrawTexture(new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height), EyePatch);
            }
        }
    }
}
