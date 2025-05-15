using UnityEngine;
using System.Collections.Generic; 


public class playerLocation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private List<GameObject> minions;
    public GameObject minionPrefab;
    protected float spawnTimer = 0f;
    protected float spawnDelay = 1f;

        
    void Start()
    {
        this.minions = new List<GameObject>();
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
        if (this.minions.Count > Random.Range(7, 10)) return;

        int index = this.minions.Count + 1;

        GameObject minion = Instantiate(this.minionPrefab);
        //minion.AddComponent<Rigidbody2D>();
        minion.name = "MinionPrefab#" + index;
        minion.transform.position = transform.position;
        minion.gameObject.SetActive(true);
        this.minions.Add(minion);
    }

    void checkMinion()
    {
        GameObject minion;
        for (int i = 0; i< this.minions.Count; i++) {
            minion = this.minions[i];
            if (minion == null)  this.minions.RemoveAt(i); 

        }
    }
}
