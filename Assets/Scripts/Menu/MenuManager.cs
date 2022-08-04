using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] private InputField _nameTxt;
    [SerializeField] private Button _playBtn;

    void Start(){
        _playBtn.interactable = PlayerPrefs.GetString(Constants.NAME_PLAYER,"")!="";
        _nameTxt.text = PlayerPrefs.GetString(Constants.NAME_PLAYER,"");
    }
    
    public void OnChangedName(){
        if(_nameTxt.text != ""){
            _playBtn.interactable = true;
        }else{
            _playBtn.interactable = false;
        }
    }
    public void OnPlayBtnClicked(){
        if(_nameTxt.text != ""){
            PlayerPrefs.SetString(Constants.NAME_PLAYER, _nameTxt.text);
            SceneManager.LoadScene("Lobby");
        }
    }
}