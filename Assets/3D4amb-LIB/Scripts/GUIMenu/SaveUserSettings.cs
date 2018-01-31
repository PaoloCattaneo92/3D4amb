using Assets._3D4amb_LIB;
using Assets._3D4amb_LIB.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveUserSettings : MonoBehaviour
{
    public GameObject difficultySlider;
    public GameObject eyeSlider;
    public GameObject PrefManager;

    public void SaveSettings()
    {
        GameDifficulty setDiff = (GameDifficulty)difficultySlider.GetComponent<Slider>().value;
        Eye setEye =                (Eye)eyeSlider.GetComponent<Slider>().value;
        PrefManager.GetComponent<PrefManager>().saveNewSettings(setDiff, setEye);
    }
}
