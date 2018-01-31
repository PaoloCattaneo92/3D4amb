using Assets._3D4amb_LIB;
using Assets._3D4amb_LIB.Scripts.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PrefManager : MonoBehaviour
{
    public UserSettings userSettings;
    public PlayerID actualPlayer;
    public SessionResult[] results;

    private string lastPlayerKey = "lpkey";
    private string allUserSettKey = "allusettkey";
    private string sessResultsKey = "sesreskey";
    private string uskeyAndName;
    private string srkeyAndName;
    private string allPlayersKey = "alplkey";

    //[HideInInspector]
    public PlayerID[] allPlayers;

    public Sprite[] allAvatars;


    // Use this for initialization
    void Awake ()
    {
        DontDestroyOnLoad(gameObject);
        loadLastPlayer();
        loadUserSettings();
        loadAllPlayers();
    }

    public void loadAllPlayers()
    {
        string p = PlayerPrefs.GetString(allPlayersKey);
        allPlayers = JsonHelper.FromJson<PlayerID>(p);
        if (allPlayers.Length > 0)
        {
            Debug.Log("allPlayers loaded: " + p);
            Debug.Log("Length of AllPlayers: " + allPlayers.Length);
        }
        else
        {
            Debug.Log("No Players to load");
        }
    }

    public void saveNewPlayer(PlayerID p)
    {
        List<PlayerID> list;
        if(allPlayers!=null && allPlayers.Length>0)
        {
            list = allPlayers.ToList();
        }
        else
        {
            list = new List<PlayerID>();
        }
        list.Add(p);
        PlayerID[] arr = list.ToArray();
        string jsoned = JsonHelper.ToJson(arr);
        PlayerPrefs.SetString(allPlayersKey, jsoned);
        actualPlayer = p;
        loadAllPlayers();
    }

    public void saveSessionResult(SessionResult sr)
    {
        loadSessionResults();                        //TODO this ToList into ToArray is teribbbbbile
        List<SessionResult> list;
        if (results != null)
        {
            list = results.ToList();    
        }
        else
        {
            list = new List<SessionResult>();
        }
        list.Add(sr);
        results = list.ToArray();
        string jsoned = JsonHelper.ToJson(results);
        PlayerPrefs.SetString(srkeyAndName, jsoned);
    }

    public void loadSessionResults()
    {
        string r = PlayerPrefs.GetString(srkeyAndName);
        if (r.Length>0)
        {
            results = JsonHelper.FromJson<SessionResult>(r);
        }
        else
        {
            Debug.Log("No results for the actual player");
        }
    }

    private void loadUserSettings()
    {
        string u = PlayerPrefs.GetString(uskeyAndName);
        if (u.Length != 0)
        {
            JsonUtility.FromJsonOverwrite(u, userSettings);
        }
        else
        {
            Debug.Log("No UserSettings for this Player. Default settings created.");
            userSettings = new UserSettings();
            PlayerPrefs.SetString(uskeyAndName, JsonUtility.ToJson(actualPlayer));
        }
    }
     
    public void saveNewSettings(GameDifficulty diff, Eye eye)
    {
        UserSettings save = new UserSettings(actualPlayer, eye, diff);
        userSettings = save;
        PlayerPrefs.SetString(uskeyAndName, JsonUtility.ToJson(save));
        Debug.Log("Saved new User Settings in " + uskeyAndName);
    }

    private void loadLastPlayer()
    {
        string p = PlayerPrefs.GetString(lastPlayerKey);
        if(p.Length!=0)
        {
            JsonUtility.FromJsonOverwrite(p, actualPlayer);
        }
        else
        {
            Debug.Log("No Player found in PlayerPrefs");
            actualPlayer = new PlayerID("New Player");
            PlayerPrefs.SetString(lastPlayerKey, JsonUtility.ToJson(actualPlayer));
            saveNewPlayer(actualPlayer);
        }
        uskeyAndName = allUserSettKey + "_" + actualPlayer.PlayerName;
        srkeyAndName = sessResultsKey + "_" + actualPlayer.PlayerName;
    }
	
}
