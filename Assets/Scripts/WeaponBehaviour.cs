using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class WeaponBehaviour : MonoBehaviour
{
    public float damage;
    public float range;
    public float fireRate;
    private bool allowFire = true;
    private ParticleSystem muzzleFlash;
    private AudioSource firingSound;

    // Start is called before the first frame update
    void Start()
    {
        this.muzzleFlash = GetComponentInChildren<ParticleSystem>();
        this.firingSound = GetComponentInChildren<AudioSource>();
        this.damage = 3.0f;
        this.range = 15.0f;
        this.fireRate = 0.1f;

        this.muzzleFlash.Stop();
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
        this.firingSound.Play();
        StartCoroutine(this.ToggleMuzzleFlash());

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

    private IEnumerator Shoot()
    {
        this.allowFire = false;
        this.Fire();
        yield return new WaitForSeconds(this.fireRate);
        this.allowFire = true;
    }

    private IEnumerator ToggleMuzzleFlash()
    {
        this.muzzleFlash.Play();
        yield return new WaitForSeconds(0.3f);
        this.muzzleFlash.Stop();
    }
}
