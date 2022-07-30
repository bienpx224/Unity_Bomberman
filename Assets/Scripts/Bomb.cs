using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;
    void OnEnable(){
        _collider.isTrigger = true;
    }
    
}