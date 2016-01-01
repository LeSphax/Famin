using UnityEngine;
using System.Collections;

public class NetworkManager : Photon.PunBehaviour
{
    private const string typeName = "Famine";
    private const string roomName = "PREPARETOFIGHT";
    RoomInfo[] roomList;

    enum NetworkState
    {
        IDLE,
        IN_LOBBY,
        WAITING_IN_ROOM,
        PLAYING
    }

    private NetworkState state;

    void Awake()
    {
        Time.fixedDeltaTime = 1.0f;
        GameObject.DontDestroyOnLoad(this);
    }

    public override void OnJoinedLobby()
    {
        state = NetworkState.IN_LOBBY;

    }

    void Start()
    {
        state = NetworkState.IDLE;
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        state = NetworkState.PLAYING;
        Application.LoadLevel("Main");
        switch (state)
        {
            case NetworkState.IDLE:
                Debug.LogError("Players shoudln't connect while we aren't in lobby");
                break;
            case NetworkState.IN_LOBBY:
                Debug.LogError("Players shoudln't connect while we aren't in a room");
                break;
            case NetworkState.WAITING_IN_ROOM:
                state = NetworkState.PLAYING;
                Application.LoadLevel("Main");
                break;
            case NetworkState.PLAYING:
                Debug.LogError("Players shoudln't connect while we are playing");
                break;
        }
    }


    void OnGUI()
    {
        switch (state)
        {
            case NetworkState.IDLE:
                GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
                break;
            case NetworkState.IN_LOBBY:
                GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
                GUI.Label(new Rect(500, 0, 150, 50), "ROOMS");
                roomList = PhotonNetwork.GetRoomList();
                int y = 60;
                foreach (RoomInfo room in roomList)
                {
                    if (GUI.Button(new Rect(350, y, 300, 50), room.name))
                    {
                        PhotonNetwork.JoinRoom(room.name);
                        state = NetworkState.PLAYING;
                        Application.LoadLevel("Main");
                    }
                    y += 60;
                }
                if (GUI.Button(new Rect(10, 60, 300, 50), "CreateNewRoom"))
                {
                    PhotonNetwork.CreateRoom(roomName);
                    state = NetworkState.WAITING_IN_ROOM;
                }
                break;
            case NetworkState.WAITING_IN_ROOM:
                GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
                GUI.Label(new Rect(500, 0, 150, 50), "Waiting for another player");
                break;
            case NetworkState.PLAYING:
                break;
        }
    }

}
