using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wave : MonoBehaviour
{
    public bool canSpawn;
    public float spawnInterval;
    public int currentWave;
    private int initWave;
    private TMP_Text waveText;

    // Start is called before the first frame update
    void Start()
    {
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
            StartCoroutine(ChangeWave());
        }
    }

    private void SetWave()
    {
        this.currentWave++;
        this.waveText.text = "Wave \n" + this.currentWave;
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
