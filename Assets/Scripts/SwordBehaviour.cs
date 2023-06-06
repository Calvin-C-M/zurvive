using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : WeaponBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        base.Init();
    }

    // Update is called once per frame
    void Update()
    {
        this.Controls();
    }

    private void Controls()
    {
        if(Time.timeScale > 0)
        {
            if(Input.GetMouseButton(0) && this.allowFire)
            {
                StartCoroutine(this.Swing());
            }
        }
    }

    private void Attack()
    {
        this.HitZombie();
    }

    private IEnumerator Swing()
    {
        this.allowFire = false;
        this.Attack();
        yield return new WaitForSeconds(this.fireRate);
        this.allowFire = true;
    }
}
