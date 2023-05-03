using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class WeaponBehaviour : MonoBehaviour
{
    public float damage;
    public float range;
    public float fireRate;
    public int ammo;
    private int initAmmo;
    private bool allowFire = true;
    private bool isReloading = false;
    private ParticleSystem muzzleFlash;
    private AudioSource firingSound;
    private AudioSource reloadSound;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] sounds = GetComponentsInChildren<AudioSource>();

        this.muzzleFlash = GetComponentInChildren<ParticleSystem>();
        this.firingSound = sounds[0];
        this.reloadSound = sounds[1];
        this.damage = 3.0f;
        this.range = 15.0f;
        this.fireRate = 0.1f;
        this.initAmmo = 30;
        this.ammo = this.initAmmo;

        this.muzzleFlash.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        this.Controls();
    }

    private void Controls()
    {
        if(Input.GetMouseButton(0) && this.allowFire && this.ammo > 0 && !this.isReloading)
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
        this.ammo -= 1;
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
                zombieAttr.TakeDamage(this.damage);
                if(zombieAttr.health <= 0)
                {
                    Destroy(zombie);
                }
            }
        }
    }

    private void Reload()
    {
        StartCoroutine(this.PlayReload());
        this.ammo = this.initAmmo;
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

    private IEnumerator PlayReload()
    {
        this.isReloading = true;
        this.reloadSound.Play();
        yield return new WaitForSeconds(1.0f);
        this.isReloading = false;
        this.reloadSound.Stop();
    }
}
