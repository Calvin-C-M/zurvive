using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItems : MonoBehaviour
{
    private float addHealthVal;
    // Start is called before the first frame update
    void Start()
    {
        this.addHealthVal = 5.0f;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            GameObject player = collision.gameObject;
            CharacterBehaviour playerAttr = player.GetComponentInChildren<CharacterBehaviour>();

            if(playerAttr.health < playerAttr.initHealth)
            {
                playerAttr.Heal(this.addHealthVal);

                Destroy(this.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
