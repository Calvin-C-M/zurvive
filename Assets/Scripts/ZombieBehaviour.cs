using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviour : MonoBehaviour
{
    private GameObject player;
    public float speed = 1.0f;

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

    private void Movement()
    {
        this.transform.LookAt(player.transform);
        this.transform.Translate(Vector3.forward * this.speed * Time.deltaTime);
    }
}
