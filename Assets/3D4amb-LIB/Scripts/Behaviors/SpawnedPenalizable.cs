using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Only GameObjects that are not present in the scene when it's first loaded
/// need this.
/// </summary>
public class SpawnedPenalizable : MonoBehaviour {

    // Use this for initialization
    void Awake()
    {
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("MainCamera");
        foreach (GameObject c in cameras)
        {
            c.GetComponent<CameraPenalizer>().AddIfPenalizable(this.gameObject);
        }
	}
}
