using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int totalZombies;
    public GameObject zombie;
    private Wave waveMaster;


    // Start is called before the first frame update
    void Start()
    {
        GameObject waveObject = GameObject.Find("WaveMaster");
        this.waveMaster = waveObject.GetComponentInChildren<Wave>();

        this.totalZombies = 3;
        this.zombie = Resources.Load("Enemy") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.waveMaster.canSpawn)
        {
            this.SpawnZombie();
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
}
