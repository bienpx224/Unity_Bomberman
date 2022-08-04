using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : Singleton<LobbyManager>
{
    [SerializeField] public LobbyNetworkManager _lobbyNetworkManager;

    public LobbyNetworkManager LobbyNetworkManager{
        get { return _lobbyNetworkManager; }
    }


}