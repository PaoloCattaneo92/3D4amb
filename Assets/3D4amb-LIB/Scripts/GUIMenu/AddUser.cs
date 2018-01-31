using Assets._3D4amb_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddUser : MonoBehaviour {

    public GameObject PrefManager;
    public GameObject InputFieldGo;

	public void Add()
    {
        string name = InputFieldGo.GetComponent<InputField>().text;
        PrefManager.GetComponent<PrefManager>().saveNewPlayer(new PlayerID(name));
    }
}
