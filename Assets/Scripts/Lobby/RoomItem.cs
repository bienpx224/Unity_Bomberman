using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    [SerializeField] private Text _nameTxt;
    [SerializeField] private Text _amountTxt;
    
    public void SetupData(string name, string amount = "", System.Action onClick = null){
        _nameTxt.text = name;
        _amountTxt.text = amount;
        gameObject.SetActive(true);
    }

    public void OnJoinBtnClicked(){
        Debug.Log("Clicked join to Room: "+ _nameTxt.text);
        LobbyManager.Instance.LobbyNetworkManager.JoinRoom(_nameTxt.text);
    }
}