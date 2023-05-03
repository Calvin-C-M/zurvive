using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZombieBehaviour : MonoBehaviour
{
    public float speed;
    public float health;
    public float attackDmg;
    public float attackRate;
    private bool allowAttack = true;

    private GameObject player;
    private TMP_Text healthText;
    private float initHealth;
    private CapsuleCollider selfCollider;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Player");
        this.healthText = GetComponentInChildren<TMP_Text>();
        this.selfCollider = GetComponent<CapsuleCollider>();

        this.speed = 1.0f;
        this.initHealth = 30.0f;
        this.health = this.initHealth;
        this.attackDmg = 5.0f;
        this.attackRate = 1.0f;
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.name == "Player" && this.allowAttack)
        {
            GameObject player = collision.gameObject;
            StartCoroutine(this.Attack(player));
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.Movement();
    }

    IEnumerator Attack(GameObject player)
    {
        this.allowAttack = false;
        CharacterBehaviour playerAttr = player.GetComponent<CharacterBehaviour>();
        if(playerAttr != null)
        {
            playerAttr.TakeDamage(this.attackDmg);
            Debug.Log(playerAttr.health);
        }

        yield return new WaitForSeconds(this.attackRate);
        this.allowAttack = true;
    }

    public void TakeDamage()
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
