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
    public GameObject pauseMenu;
    public GameObject gameOver;

    private float initHealth;
    private Rigidbody rb;
    private Camera cam;
    private TMP_Text killCounterText;
    private TMP_Text healthText;
    private GameObject primary;
    private GameObject secondary;
    private GameObject ground;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;

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

        this.pauseMenu.SetActive(false);
        this.gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        this.KeyInput();
        this.killCounterText.text = this.killCounter.ToString(); // Update kill counter text
        this.UpdatePanelStats();
        if(this.health <= 0)
        {
            Debug.Log("Player is dead");
            this.gameOver.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0.0f;

            // Destroy(this.gameObject);
        }
    }

    private void KeyInput()
    {
        if(Time.timeScale > 0)
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
        
        if(Input.GetKeyDown(KeyCode.Escape)) // Pausing Game
        {
            this.pauseMenu.SetActive(!this.pauseMenu.activeSelf);
            Cursor.visible = this.pauseMenu.activeSelf;
            Cursor.lockState = (this.pauseMenu.activeSelf) ? CursorLockMode.None : CursorLockMode.Locked;
            Time.timeScale = (this.pauseMenu.activeSelf) ? 0.0f : 1.0f;
        }
    }

    private void UpdatePanelStats()
    {
        TMP_Text pauseWaveCounter = this.pauseMenu.GetComponentsInChildren<TMP_Text>()[0];
        TMP_Text pauseKillCounter = this.pauseMenu.GetComponentsInChildren<TMP_Text>()[1];
        TMP_Text endKillCounter = this.gameOver.GetComponentsInChildren<TMP_Text>()[1];

        GameObject waveMaster = GameObject.Find("WaveMaster");
        Wave wave = waveMaster.GetComponentInChildren<Wave>();

        pauseKillCounter.text = "Kills: " + this.killCounter;
        pauseWaveCounter.text = "Wave: " + wave.currentWave;
        endKillCounter.text = "Kills: " + this.killCounter;
    }

    public void TakeDamage(float damage)
    {
        this.health -= damage;
        float currHealth = (this.health / this.initHealth) * 100;
        this.healthText.text = Mathf.Round(currHealth) + "%";
    }
}
