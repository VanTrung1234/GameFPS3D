using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZombieSpawnControlr : MonoBehaviour
{

    public int initialZombiesPerWave = 5;
    public int currentZombiesPerWave;

    public float spawnDelay = 0.5f; // Delay between spawning each zombie in a wave;

    public int currentWave = 0;
    public float waveCooldown = 10.0f; // Time in seconds between waves;

    public bool inCooldown;
    public float cooldownCounter = 0; // We only use this for testing and the UI;

    public List<Zombie> currentZombiesAlive;

    public GameObject zombiePrefab;

    public TextMeshProUGUI waveOverUI;
    public TextMeshProUGUI cooldownCountUI;
    public TextMeshProUGUI waveUI;


    private void Start()
    {
        currentZombiesPerWave = initialZombiesPerWave;
        GobalReferences.instance.waveNumber = currentWave;
        StartNextWave();
    }

    private void StartNextWave()
    {
        currentZombiesAlive.Clear();
        currentWave++;
        GobalReferences.instance.waveNumber = currentWave;
        waveUI.text = "WAVE:" + currentWave.ToString();
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < currentZombiesPerWave; i++)
        {
            
            Vector3 spawnOffset = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0f, UnityEngine.Random.Range(-1f, 1f));
            Vector3 spawnPosition = transform.position + spawnOffset;
            var zombie=Instantiate(zombiePrefab, spawnPosition,Quaternion.identity);

            Zombie zombieScript=zombie.GetComponent<Zombie>();

            currentZombiesAlive.Add(zombieScript);
            yield return new WaitForSeconds(spawnDelay);


        }
    }
    private void Update()
    {
        List<Zombie> zombieToRemove=new List<Zombie> ();
        foreach (Zombie zombie in currentZombiesAlive)
        {
            if (zombie.isDead)
            {
                zombieToRemove.Add(zombie);
            }
        }
        foreach (Zombie zombie in zombieToRemove)
        {

            currentZombiesAlive.Remove(zombie);
        }
        zombieToRemove.Clear();

        if(currentZombiesAlive.Count==0&& inCooldown == false)
        {
            StartCoroutine(WaveCooldown());
        }
        if (inCooldown)
        {
            cooldownCounter -= Time.deltaTime;
        }
        else
        {
            cooldownCounter = waveCooldown;
        }
        cooldownCountUI.text=cooldownCounter.ToString("F0");
    }

    private IEnumerator WaveCooldown()
    {
        inCooldown = true;
        waveOverUI.gameObject.SetActive(true); 
        yield return new WaitForSeconds(waveCooldown);

        inCooldown = false;
        waveOverUI.gameObject.SetActive(false);
        currentZombiesPerWave *= 2;
        StartNextWave();
    }
}
