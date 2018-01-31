using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject[] spawns;

    private int i = 0;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(spawnThings());
	}
	
	// Update is called once per frame
	void Update ()
    {
    }

    IEnumerator spawnThings()
    {
        //    while (true)
        //    {
        yield return new WaitForSeconds(1);
            transform.position = new Vector2(Random.Range(-3, 3), Random.Range(-3, 3));
            i = (i + 1) % 3;
            //GameObject.Instantiate(spawns[i], this.transform);
            GameObject.Instantiate(spawns[0], this.transform);
        //}
    }
}
