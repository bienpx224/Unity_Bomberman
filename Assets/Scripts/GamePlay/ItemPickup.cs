using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType {
        ExtraBomb, BlastRadius, SpeedUp
    };
    public ItemType itemType;
    
    public void OnItemPickup(GameObject player){

                /* Get scripts from GameObject and do something */
        switch (itemType) {
            case ItemType.ExtraBomb:
                player.GetComponent<BombController>().AddBomb();
                break;
            case ItemType.BlastRadius:
                player.GetComponent<BombController>().explosionRadius++;
                break;
            case ItemType.SpeedUp:
                player.GetComponent<MovementController>().speed++;
            break;
        }

        Lean.Pool.LeanPool.Despawn(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other){
        /* Check if Player Tag collides with ItemPickup */
        // Debug.Log(LayerMask.LayerToName(other.gameObject.layer));
        if(other.CompareTag("Player")){
            OnItemPickup(other.gameObject);
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Explosion")){
            Lean.Pool.LeanPool.Despawn(gameObject);
        }
    }
}