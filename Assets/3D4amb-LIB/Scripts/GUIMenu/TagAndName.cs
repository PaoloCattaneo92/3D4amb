using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagAndName : MonoBehaviour {

    public GameObject PrefManager;
    public string LabelTag;

	// Use this for initialization
	void Start ()
    {
        updateName();
	}

    void updateName()
    {
        string name = PrefManager.GetComponent<PrefManager>().actualPlayer.PlayerName;
        gameObject.GetComponent<Text>().text = LabelTag + name;
    }

    void OnEnable()
    {
        updateName();
    }
	
}
