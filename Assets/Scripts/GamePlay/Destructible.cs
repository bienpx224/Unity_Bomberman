using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Destructible : MonoBehaviour
{
    public float destructionTime = 1f;
    [Range(0f, 1f)]  // Make a slider to choose value
    public float itemSpawnChance = 0.2f;
    public GameObject[] spawnableItems;

    void Start()
    {
        Destroy(gameObject, destructionTime);
    }

    private void OnDestroy()
    {
        if (spawnableItems.Length > 0 && Random.value < itemSpawnChance)
        {
            int randomIndex = Random.Range(0, spawnableItems.Length);
            PhotonNetwork.Instantiate(Constants.PREFABS_PATH + spawnableItems[randomIndex].name, transform.position, Quaternion.identity);
        }
    }

}