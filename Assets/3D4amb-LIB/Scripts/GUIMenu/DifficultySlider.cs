using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySlider : MonoBehaviour {

    public GameObject ItsLabel;
    public GameObject PrefManager;

    private string SliderTag = "Difficulty: ";
    private string text;
	
    void Start()
    {
        gameObject.GetComponent<Slider>().value = (int)PrefManager.GetComponent<PrefManager>().userSettings.defaultDifficulty;
    }


	public void UpdateSlider()
    {
        float v = gameObject.GetComponent<Slider>().value;
        if(v==0)
        {
            text = SliderTag + "EASY";
        }
        else if(v==1)
        {
            text = SliderTag + "MEDIUM";
        }
        else if(v==2)
        {
            text = SliderTag + "HARD";
        }
        else
        {
            text = SliderTag + "???";
        }
        ItsLabel.GetComponent<Text>().text = text;
    }
}
