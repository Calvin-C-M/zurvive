using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    private Camera cam;
    private GameObject ground;
    public float walkingSpd = 10.0f;
    public float runningSpd = 5.0f;
    public float jumpHeight = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.cam = Camera.main;
        this.ground = GameObject.Find("Map");
    }

    // Update is called once per frame
    void Update()
    {
        this.Movement();
    }

    void Movement()
    {
        float velocity = this.walkingSpd;
        
        if(Input.GetKey(KeyCode.LeftShift)) // Running
        {
            velocity = this.walkingSpd + this.runningSpd;
        }
        if(Input.GetKey("w")) // Move forwards
        {
            this.transform.Translate(Vector3.forward * velocity * Time.deltaTime);
        }
        if(Input.GetKey("a")) // Strafe left
        {
            this.transform.Translate(Vector3.left * velocity * Time.deltaTime);
        }
        if(Input.GetKey("s")) // Move backwards
        {
            this.transform.Translate(Vector3.back * velocity * Time.deltaTime);
        }
        if(Input.GetKey("d")) // Strafe right
        {
            this.transform.Translate(Vector3.right * velocity * Time.deltaTime);
        }
        if(Input.GetKeyDown(KeyCode.Space)) // Jumping
        {
            this.rb.velocity = new Vector3(0,this.jumpHeight,0);
        }

        // Debug Keys
        if(Input.GetKeyDown(KeyCode.F1))
        {
            this.cam.transform.Translate(0,1,-5);
        }
    }
}
