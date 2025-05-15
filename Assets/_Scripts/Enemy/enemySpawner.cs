using UnityEngine;
using System.Collections.Generic;
public class enemySpawner : Spawner
{
    [Header("Enemy")]
    public int max = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        this.objPrefab.SetActive(false);
        this.objects = new List<GameObject>();

    }
    // Update is called once per frame
    void Update()
    {
        check();
        spawn();
    }
    void spawn()
    {
        if(PlayerCtrl.Instance.hp.checkDead()) { return; }

        if(objects.Count >= this.max) { return; }

        this.spawnTimer += Time.deltaTime;
        if (spawnTimer < spawnDelay) return;
        spawnTimer = 0f;

        GameObject enemy= Instantiate(this.objPrefab);
        enemy.transform.position=SpawnPos.transform.position;
        enemy.transform.parent = transform;
        enemy.SetActive(true);

        objects.Add(enemy);
    }
    void check()
    {
        GameObject en;
        for (int i = 0; i < this.objects.Count; i++)
        {
            en = this.objects[i];
            if (en == null) this.objects.RemoveAt(i);
            Debug.Log(objects.Count);

        }
    }
}
