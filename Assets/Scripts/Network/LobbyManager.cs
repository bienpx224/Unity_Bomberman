using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : Singleton<LobbyManager>
{
    [SerializeField] public LobbyNetworkManager _lobbyNetworkManager;

    public LobbyNetworkManager LobbyNetworkManager{
        get { return _lobbyNetworkManager; }
    }

    public void PlayGame(){
        SceneManager.LoadScene("Bomberman");
    }
    public void BackToMenu(){
        SceneManager.LoadScene("Menu");
    }
}