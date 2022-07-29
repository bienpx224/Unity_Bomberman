using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        // Debug.Log("OnTriggerStay2D: "+ LayerMask.LayerToName(other.gameObject.layer));

    }

}
