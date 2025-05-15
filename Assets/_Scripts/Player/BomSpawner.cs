using UnityEngine;
using System.Collections.Generic; 


public class BomSpawner : Spawner
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //[Header("Bom")]
    

        
    void Start()
    {
        this.objects = new List<GameObject>();
    }




    // Update is called once per frame
    void Update()
    {

       
        this.checkMinion();
        this.Spawn();

        
    }

    void Spawn()
    {
        this.spawnTimer += Time.deltaTime;
        if (this.spawnTimer < this.spawnDelay) return;
        this.spawnTimer = 0;
        if (this.objects.Count > Random.Range(7, 10)) return;

        int index = this.objects.Count + 1;

        GameObject minion = Instantiate(this.objPrefab);
        //minion.AddComponent<Rigidbody2D>();
        minion.name = "MinionPrefab#" + index;
        minion.transform.position = this.SpawnPos.transform.position;
        minion.gameObject.SetActive(true);
        minion.transform.parent = transform;
        this.objects.Add(minion);
        
    }

    void checkMinion()
    {
        GameObject minion;
        for (int i = 0; i< this.objects.Count; i++) {
            minion = this.objects[i];
            if (minion == null)  this.objects.RemoveAt(i); 

        }
    }
}
