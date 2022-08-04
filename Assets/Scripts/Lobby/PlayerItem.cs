using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    [SerializeField] private Text _nameTxt;
    [SerializeField] private Image _avatarImg;
    
    public void SetupData(string name, System.Action onClick = null){
        _nameTxt.text = name;
        gameObject.SetActive(true);
    }
}