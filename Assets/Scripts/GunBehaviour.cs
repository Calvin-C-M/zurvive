using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunBehaviour : WeaponBehaviour
{
    protected bool isReloading;
    protected TMP_Text ammoText;
    protected ParticleSystem[] muzzleParticles;
    protected GameObject muzzleLight;
    protected AudioSource firingSound;
    protected AudioSource reloadSound;
    
    // Start is called before the first frame update
    void Start() 
    {
        base.Init();
        this.isReloading = false;

        // Control muzzle particle attributes
        this.muzzleParticles = GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem particle in this.muzzleParticles)
        {
            particle.Stop();
        }        
        this.muzzleLight = GameObject.Find("Light");
        this.muzzleLight.SetActive(false);

        // Control weapon sound components
        AudioSource[] sounds = GetComponentsInChildren<AudioSource>();
        foreach(AudioSource sound in sounds) 
        {
            sound.volume = 0.5f;
        }
        this.firingSound = sounds[0];
        this.reloadSound = sounds[1];

        this.ammoText = GetComponentInChildren<TMP_Text>();
        this.ChangeAmmoText();
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
            if(Input.GetMouseButton(0) && this.allowFire && this.ammo > 0 && !this.isReloading)
            {
                StartCoroutine(this.Shoot());
            }
            if(Input.GetKeyDown("r"))
            {
                this.Reload();
            }
        }
    }

    private void Fire()
    {
        this.ammo -= 1;
        this.ChangeAmmoText();
        this.firingSound.Play();
        StartCoroutine(this.ToggleMuzzleFlash());
        this.HitZombie();
    }

    private void Reload()
    {
        StartCoroutine(this.PlayReload());
        this.ammo = this.initAmmo;
        this.ChangeAmmoText();
    }

    private IEnumerator PlayReload()
    {
        this.isReloading = true;
        this.reloadSound.Play();
        yield return new WaitForSeconds(1.0f);
        this.isReloading = false;
        this.reloadSound.Stop();
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
        foreach(ParticleSystem particle in this.muzzleParticles)
        {
            particle.Play();
        }
        this.muzzleLight.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        foreach(ParticleSystem particle in this.muzzleParticles)
        {
            particle.Stop();
        }
        this.muzzleLight.SetActive(false);
    }
    
    private void ChangeAmmoText()
    {
        this.ammoText.text = this.ammo + " / " + this.initAmmo;
    }
}
