using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[SerializeField]
public class WeaponBehaviour : MonoBehaviour
{
    public string weaponType;
    public float damage;
    public float range;
    public float fireRate;
    public float ammo;
    private float initAmmo;
    private bool allowFire = true;
    private bool isReloading = false;
    private ParticleSystem[] muzzleParticles;
    private GameObject muzzleLight;
    private AudioSource firingSound;
    private AudioSource reloadSound;
    private TMP_Text ammoText;
    private WeaponDict weaponDictionary;


    // Start is called before the first frame update
    void Start()
    {
        // Control weapon particle components
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

        // Control weapon attributes
        this.weaponType = (this.weaponType == "") ? "rifle" : this.weaponType.ToLower(); 
        this.weaponDictionary = new WeaponDict();
        this.damage = this.weaponDictionary.attribute[weaponType]["damage"];
        this.range = this.weaponDictionary.attribute[weaponType]["range"];
        this.fireRate = this.weaponDictionary.attribute[weaponType]["fireRate"];
        this.initAmmo = this.weaponDictionary.attribute[weaponType]["initAmmo"];
        this.ammo = this.initAmmo;
        
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
        this.ChangeAmmoText();
        this.firingSound.Play();
        StartCoroutine(this.ToggleMuzzleFlash());

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, this.range))
        {
            GameObject zombie = hit.collider.gameObject;
            ZombieBehaviour zombieAttr = zombie.GetComponent<ZombieBehaviour>();
            CharacterBehaviour characterAttr = GetComponentInParent<CharacterBehaviour>();
            if(zombieAttr != null)
            {
                zombieAttr.TakeDamage(this.damage);
                if(zombieAttr.health <= 0)
                {
                    Destroy(zombie);
                    characterAttr.killCounter++;
                }
            }
        }
    }

    private void Reload()
    {
        StartCoroutine(this.PlayReload());
        this.ammo = this.initAmmo;
        this.ChangeAmmoText();
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

    private IEnumerator PlayReload()
    {
        this.isReloading = true;
        this.reloadSound.Play();
        yield return new WaitForSeconds(1.0f);
        this.isReloading = false;
        this.reloadSound.Stop();
    }

    private void ChangeAmmoText()
    {
        this.ammoText.text = this.ammo + " / " + this.initAmmo;
    }
}
