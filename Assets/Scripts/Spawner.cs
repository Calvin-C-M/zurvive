using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    public int totalZombies;

    public GameObject zombie;
    public float spawnInterval; // In minutes
    public int currentWave;
    private int initWave;
    private TMP_Text waveText;
    private bool canSpawn;


    // Start is called before the first frame update
    void Start()
    {
        this.totalZombies = 3;
        this.spawnInterval = 2f;
        this.initWave = 0;
        this.currentWave = this.initWave;
        this.waveText = GetComponentInChildren<TMP_Text>();
        this.canSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.canSpawn)
        {
            StartCoroutine(this.ChangeWave());
        }
    }

    private void SpawnZombie()
    {
        Vector3 spawnPos = this.transform.position;
        for(int i=0; i<this.totalZombies; i++)
        {
            spawnPos.x += i;
            Instantiate(zombie, spawnPos, Quaternion.identity);
        }
    }

    private void SetWave()
    {
        this.currentWave++;
        this.waveText.text = "Wave \n" + this.currentWave;
        this.SpawnZombie();
    }

    private IEnumerator ChangeWave()
    {
        this.SetWave();
        float second = 6f;
        this.canSpawn = false;
        yield return new WaitForSeconds(this.spawnInterval * second);
        this.canSpawn = true;
    }
}
