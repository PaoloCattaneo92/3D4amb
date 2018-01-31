using Assets._3D4amb_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPenalizer : MonoBehaviour {

    public Eye eye;
    public PenaltyManager PM;
    public List<GameObject> PenalizablesGos;

    /// <summary>
    /// This add the penalizable Objects that are already in the scene
    /// </summary>
    void Start()
    {
        while (PM == null)
        {
            PM = GameObject.Find("PenaltyManager").GetComponent<PenaltyManager>();
        }
        PenalizablesGos = new List<GameObject>();
        foreach (KeyValuePair<string, bool> kvp in PM.Penalizables)
        {
            GameObject[] arr;
            if (kvp.Value)
            {
                arr = GameObject.FindGameObjectsWithTag(kvp.Key);
                foreach (GameObject go in arr)
                {
                    PenalizablesGos.Add(go);
                }
            }
        }
    }

    public void AddIfPenalizable(GameObject go)
    {
        if(PM!=null)
        {
            foreach (KeyValuePair<string, bool> kvp in PM.Penalizables)
            {
                if (kvp.Value && kvp.Key.Equals(go.tag))
                {
                    PenalizablesGos.Add(go);
                }
            }
        }
    }

    private void OnPreRender()
    {   
        if (PM.PenaltyInfoNow.PenaltyEye == eye)
        {
            foreach (GameObject go in PenalizablesGos)
            {
                go.GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1, 1-PM.PenaltyInfoNow.PenaltyTransparency);
            }
        }
        else
        {
            foreach (GameObject go in PenalizablesGos)
            {
                go.GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1, 1);
            }
        }
    }
}
