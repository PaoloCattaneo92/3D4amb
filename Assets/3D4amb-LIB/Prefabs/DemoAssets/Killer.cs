using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(killPlayer());
    }

    IEnumerator killPlayer()
    {
        Debug.Log("kill starting...");
        yield return new WaitForSeconds(5);
        PenaltyTrigger PT = GameObject.Find("PenaltyManager").GetComponent<PenaltyTrigger>();
        PT.EventDeath();
        Debug.Log("killed");
    }
}
