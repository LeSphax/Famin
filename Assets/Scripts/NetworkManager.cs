using UnityEngine;
using System.Collections;

public class NetworkManager : Photon.MonoBehaviour
{
    private const string typeName = "DuelClicker";
    private const string gameName = "IMMAKICKYOURTINYASS";

    void Awake()
    {
        GameObject.DontDestroyOnLoad(this);
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Can't join random room!");
        PhotonNetwork.CreateRoom(gameName);
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

}
