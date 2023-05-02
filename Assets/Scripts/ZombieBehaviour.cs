using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviour : MonoBehaviour
{
    private GameObject player;
    public float speed = 1.0f;
    public float health = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.Movement();
    }

    public void Damage()
    {
        // Debug.Log("Zombie is damaged");
        this.health -= 10.0f;
    }

    private void Movement()
    {
        this.transform.LookAt(player.transform);
        this.transform.Translate(Vector3.forward * this.speed * Time.deltaTime);
    }
}
