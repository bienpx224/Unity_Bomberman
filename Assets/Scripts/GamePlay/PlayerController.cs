using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    public enum TYPE_PLAYER {
        BLACK,BLUE,RED,WHITE
    }
    private MovementController _movementController;
    [SerializeField] private TYPE_PLAYER _typePlayer = TYPE_PLAYER.BLACK; 
    [SerializeField] private PhotonView _photonView;
    public TYPE_PLAYER TypePlayer { get;}
    void Start(){
        _movementController = GetComponent<MovementController>();
        _photonView = GetComponent<PhotonView>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        // Debug.Log("OnTriggerStay2D: "+ LayerMask.LayerToName(other.gameObject.layer));

    }

}
