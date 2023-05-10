using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int totalZombies;

    public GameObject zombie;

    // Start is called before the first frame update
    void Start()
    {
        this.totalZombies = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F2))
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
