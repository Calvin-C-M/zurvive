using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.Movement();
    }

    void Movement()
    {
        if(Input.GetKey("w"))
        {
            this.transform.Translate(Vector3.forward * this.speed * Time.deltaTime);
        }
        if(Input.GetKey("a"))
        {
            this.transform.Translate(Vector3.left * this.speed * Time.deltaTime);
        }
        if(Input.GetKey("s"))
        {
            this.transform.Translate(Vector3.back * this.speed * Time.deltaTime);
        }
        if(Input.GetKey("d"))
        {
            this.transform.Translate(Vector3.right * this.speed * Time.deltaTime);
        }
    }
}
