using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public float damage = 3.0f;
    public float range = 15.0f;
    public float fireRate = 0.8f;
    private bool allowFire = true;
    private ParticleSystem muzzleFlash;

    // Start is called before the first frame update
    void Start()
    {
        this.muzzleFlash = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        this.Controls();
    }

    private void Controls()
    {
        if(Input.GetMouseButton(0) && this.allowFire)
        {
            StartCoroutine(this.Shoot());
        }
        if(Input.GetKeyDown("r"))
        {
            this.Reload();
        }
    }

    private void Fire()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, this.range))
        {
            GameObject zombie = hit.collider.gameObject;
            ZombieBehaviour zombieAttr = zombie.GetComponent<ZombieBehaviour>();
            if(zombieAttr != null)
            {
                zombieAttr.Damage();
                if(zombieAttr.health <= 0)
                {
                    Destroy(zombie);
                }
            }
        }
    }

    private void Reload()
    {

    }

    IEnumerator Shoot()
    {
        this.allowFire = false;
        this.Fire();
        yield return new WaitForSeconds(this.fireRate);
        this.allowFire = true;
    }
}
