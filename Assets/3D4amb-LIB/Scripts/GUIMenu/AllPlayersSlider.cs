using Assets._3D4amb_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllPlayersSlider : MonoBehaviour
{

    public GameObject PrefManager;
    public GameObject PlayerAvatar;
    public GameObject PlayerName;

    private int playerIndex;
    private PlayerID actualPlayer;
    private PlayerID[] allPlayers;

    // Use this for initialization
    void Start ()
    {
        allPlayers = PrefManager.GetComponent<PrefManager>().allPlayers;
        actualPlayer = PrefManager.GetComponent<PrefManager>().actualPlayer;
        for (int i=0;i<allPlayers.Length;i++)
        {
            if (allPlayers[i].Equals(actualPlayer))
            {
                playerIndex = i;
                break;
            }
        }
        gameObject.GetComponent<Slider>().value = playerIndex;
        UpdatePlayerData();
    }

    public void UpdatePlayerData()
    {
        Debug.Log("(int)ItsSlider.GetComponent<Slider>().value = " + (int)gameObject.GetComponent<Slider>().value);
        int i = 0;
        foreach(PlayerID p in allPlayers)
        {
            Debug.Log("player " + i++ + " è " + p.PlayerName);
        }
        actualPlayer = allPlayers[(int)gameObject.GetComponent<Slider>().value];
        PlayerAvatar.GetComponent<Image>().sprite = PrefManager.GetComponent<PrefManager>().allAvatars[actualPlayer.IdAvatar];
        PlayerName.GetComponent<Text>().text = actualPlayer.PlayerName;
        gameObject.GetComponent<Slider>().maxValue = allPlayers.Length;
    }
}
