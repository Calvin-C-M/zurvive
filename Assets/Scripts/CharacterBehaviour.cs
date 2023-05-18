using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterBehaviour : MonoBehaviour
{
    public float walkingSpd;
    public float runningSpd;
    public float jumpHeight;
    public float health;
    public int killCounter;
    private Rigidbody rb;
    private Camera cam;
    private GameObject ground;
    private TMP_Text killCounterText;
    private TMP_Text healthText;
    private float initHealth;
    private GameObject primary;
    private GameObject secondary;


    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.cam = Camera.main;
        this.ground = GameObject.Find("Map");
        this.healthText = GetComponentsInChildren<TMP_Text>()[1];
        this.killCounterText = GetComponentsInChildren<TMP_Text>()[2];
        this.primary = GameObject.Find("Rifle");
        this.secondary = GameObject.Find("Sword");

        this.killCounter = 0;
        this.walkingSpd = 10.0f;
        this.runningSpd = 5.0f;
        this.jumpHeight = 5.0f;
        this.initHealth = 50.0f;
        this.health = this.initHealth;
        this.secondary.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        this.KeyInput();
        this.killCounterText.text = this.killCounter.ToString(); // Update kill counter text
        if(this.health < 0)
        {
            Debug.Log("Player is dead");
            Destroy(this.gameObject);
        }
    }

    private void KeyInput()
    {

        // ============ MOVEMENT ============
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

        // ============ SWITCH WEAPONS ============
        if(Input.GetKeyDown("1"))
        {
            this.primary.SetActive(true);
            this.secondary.SetActive(false);
        }
        if(Input.GetKeyDown("2"))
        {
            this.primary.SetActive(false);
            this.secondary.SetActive(true);
        }

        // ============ DEBUG KEYS ============
        if(Input.GetKeyDown(KeyCode.F1))
        {
            this.cam.transform.Translate(0,1,-5);
        }
    }

    public void TakeDamage(float damage)
    {
        this.health -= damage;
        float currHealth = (this.health / this.initHealth) * 100;
        this.healthText.text = Mathf.Round(currHealth) + "%";
    }
}
