using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [Header("Spawner")]
    public GameObject objPrefab;
    public GameObject SpawnPos;
    public List<GameObject> objects;
    protected float spawnTimer = 0f;
    protected float spawnDelay = 1f;
}
