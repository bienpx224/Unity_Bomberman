using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject[] players;

    public void CheckWinState(){
        int aliveCount = 0;
        foreach (GameObject player in players) {
            if(player.activeSelf){
                aliveCount++;
            }
        }
        if(aliveCount <= 1){
            Invoke(nameof(NewRound), 3f);
        }
    }
    public void NewRound(){
            // SceneManager.LoadScene("Bomberman");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
