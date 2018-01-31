using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthyEyeSlider : MonoBehaviour {

    public GameObject ItsLabel;
    public GameObject PrefManager;

    private string HealthyTag = "Healthy eye: ";
    private string text;

    void Start()
    {
        gameObject.GetComponent<Slider>().value = (int)PrefManager.GetComponent<PrefManager>().userSettings.healthyEye;
    }

    public void UpdateSlider()
    {
        float v = gameObject.GetComponent<Slider>().value;
        if (v == 0)
        {
            text = HealthyTag + "LEFT";
        }
        else if (v == 1)
        {
            text = HealthyTag + "RIGHT";
        }
        else
        {
            text = HealthyTag + "???";
        }
        ItsLabel.GetComponent<Text>().text = text;
    }
}
