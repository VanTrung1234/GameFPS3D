using UnityEngine;

public class If : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int playerLocation = 20;
        int spawnLocation = 20;
        if(playerLocation==spawnLocation) this.Spawn();
    }

    void Spawn()
    {
        Debug.Log("okkk");
    }
}
