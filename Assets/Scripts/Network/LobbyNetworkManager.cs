using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class LobbyNetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI _namePlayerTxt;
    [SerializeField] private InputField _roomInput;
    [SerializeField] private RoomItem _roomItemPrefab;
    [SerializeField] private Transform _roomListParent;
    [SerializeField] private PlayerItem _playerItemPrefab;
    [SerializeField] private Transform _playerListParent;
    [SerializeField] private TextMeshProUGUI _statusTxt;
    [SerializeField] private GameObject _leaveRoomBtn;


    private List<RoomItem> _roomList = new List<RoomItem>();
    private List<PlayerItem> _playerList = new List<PlayerItem>();
    void Start()
    {
        Initialize();
        Connect();
    }
    #region PhotonCallbacks
    public override void OnConnectedToMaster()
    {
        _statusTxt.text = "Connected to Master";
        Debug.Log("Connected to master server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("OnRoomListUpdate: " + roomList.Count);
        UpdateRoomList(roomList);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected");
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        _statusTxt.text = "In Lobby";
        _namePlayerTxt.text = PhotonNetwork.NickName;
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom: " + PhotonNetwork.CurrentRoom.Name);
        _statusTxt.text = "In Room ` " + PhotonNetwork.CurrentRoom.Name + "`";
        _leaveRoomBtn.SetActive(true);
        UpdatePlayerList();
    }
    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom: ");
        _statusTxt.text = "In Lobby";
        _leaveRoomBtn.SetActive(false);
        ClearPlayerList();
    }

    public override void OnPlayerEnteredRoom(Player player)
    {
        Debug.Log("OnPlayer Entered Room: " + player.NickName);
    }
    public override void OnPlayerLeftRoom(Player player)
    {
        Debug.Log("OnPlayer Left Room: " + player.NickName);
    }

    #endregion

    private void Initialize()
    {
        _namePlayerTxt.text = PlayerPrefs.GetString(Constants.NAME_PLAYER,"");
        _statusTxt.text = "In Connecting";
        _leaveRoomBtn.SetActive(false);
    }
    private void UpdateRoomList(List<RoomInfo> newRoomList)
    {
        // Clear the current list of rooms
        for (int i = 0; i < _roomList.Count; i++)
        {
            Destroy(_roomList[i].gameObject);
        }
        _roomList.Clear();
        // Generate a new list with the update info
        for (int i = 0; i < newRoomList.Count; i++)
        {
            // Skip empty rooms
            if (newRoomList[i].PlayerCount == 0) { continue; }

            RoomItem roomItem = Instantiate(_roomItemPrefab);
            roomItem.transform.SetParent(_roomListParent);
            roomItem.SetupData(newRoomList[i].Name);
            _roomList.Add(roomItem);
        }
    }
    private void UpdatePlayerList()
    {
        ClearPlayerList();
        // generate a new list with the update info 
        if (PhotonNetwork.CurrentRoom != null)
        {
            foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
            {
                PlayerItem playerItem = Instantiate(_playerItemPrefab);
                playerItem.transform.SetParent(_playerListParent);
                playerItem.SetupData(player.Value.NickName);
                _playerList.Add(playerItem);
            }
        }
    }
    private void ClearPlayerList()
    {
        // Clear the current list of players
        for (int i = 0; i < _playerList.Count; i++)
        {
            Destroy(_playerList[i].gameObject);
        }
        _playerList.Clear();
    }
    private void Connect()
    {
        string namePlayer = PlayerPrefs.GetString(Constants.NAME_PLAYER,"Player");
        PhotonNetwork.NickName = namePlayer + "_" + Random.Range(0, 5000);
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public void JoinRoom(string name)
    {
        PhotonNetwork.JoinRoom(name);
    }
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(_roomInput.text) == false)
        {
            PhotonNetwork.CreateRoom(_roomInput.text, new RoomOptions() { MaxPlayers = 4 }, null);
        }
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }


}