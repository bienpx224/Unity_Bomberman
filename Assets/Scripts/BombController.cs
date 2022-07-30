using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Lean.Pool;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    public GameObject bombPrefabs;
    public KeyCode inputKey = KeyCode.Space;
    public float bombFuseTime = 3f;
    public int bombAmount = 2;
    private int bombsRemaining;

    /* Explosion */
    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    [Header("Destructible")]
    public Tilemap destructibleTiles;
    public Destructible destructiblePrefab;
    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }

    private void Update()
    {
        if (bombsRemaining > 0 && Input.GetKeyDown(inputKey))
        {
            StartCoroutine(PlaceBomb());
        }
    }

    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;

        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Debug.Log(position);

        GameObject bomb = Lean.Pool.LeanPool.Spawn(bombPrefabs, position, Quaternion.identity);
        bombsRemaining--;

        yield return new WaitForSeconds(bombFuseTime);

        // End bombFuseTime, make a explosion effect
        position = bomb.transform.position;  // Get newBomb position of the bomb to create explosion effect
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        /* Make the explosion in the center, at position of the bomb */
        Explosion explosion = Lean.Pool.LeanPool.Spawn(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

        /* Make the explosion around the bomb. */
        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        Lean.Pool.LeanPool.Despawn(bomb);
        bombsRemaining++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            Debug.Log("OnTriggerExit2D of Bomb");
            other.isTrigger = false;   // Cho phép tương tác vật lý giữa Player và bom 
        }
    }

    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            return;
        }

        position += direction;  /* Change position to the side direction */

        /* Check xem vị trí vụ nổ đang ở trên LayerMask thì ko hiển thị */
        if(Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask)){
            ClearDestructible(position);
            return;
        }

        Explosion  explosion = Lean.Pool.LeanPool.Spawn(explosionPrefab, position,Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction); /* Set Rotation for explosion rotate to direction : Cho vụ nổ quay đúng hướng */
        explosion.DestroyAfter(explosionDuration);

        Explode(position, direction, length - 1); /* More explode over and over util length == 0 */

    }
    private void ClearDestructible(Vector2 position){
        Vector3Int cell = destructibleTiles.WorldToCell(position);  // Convert vector2 to cell in Tilemaps
        TileBase tile = destructibleTiles.GetTile(cell); // Get this tile at position

        if(tile != null){
            Lean.Pool.LeanPool.Spawn(destructiblePrefab, position, Quaternion.identity);
            destructibleTiles.SetTile(cell, null);   // Remove this tile from destructibleTiles. 
        }
        
    }
    public void AddBomb(){
        bombAmount ++;
        bombsRemaining++;
    }
}