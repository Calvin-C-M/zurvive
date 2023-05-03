using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZombieBehaviour : MonoBehaviour
{
    public float speed = 1.0f;
    public float health = 30.0f;

    private GameObject player;
    private TMP_Text healthText;
    private float initHealth;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Player");
        this.healthText = GetComponentInChildren<TMP_Text>();
        this.initHealth = this.health;
    }

    // Update is called once per frame
    void Update()
    {
        this.Movement();
    }

    public void Damage()
    {
        this.health -= 10.0f;
    
        float currHealth = (this.health / this.initHealth) * 100;
        this.healthText.text = Mathf.Round(currHealth) + "%";
    }

    private void Movement()
    {
        this.transform.LookAt(player.transform);
        this.transform.Translate(Vector3.forward * this.speed * Time.deltaTime);
    }
}
