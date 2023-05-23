using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class WeaponBehaviour : MonoBehaviour
{
    public string weaponType;
    public float damage;
    public float range;
    public float fireRate;
    public float ammo;
    protected float initAmmo;
    protected WeaponDict weaponDictionary;


    // Start is called before the first frame update
    protected void Init()
    {
        // Control weapon attributes
        this.weaponType = (this.weaponType == "") ? "rifle" : this.weaponType.ToLower(); 
        this.weaponDictionary = new WeaponDict();
        this.damage = this.weaponDictionary.attribute[weaponType]["damage"];
        this.range = this.weaponDictionary.attribute[weaponType]["range"];
        this.fireRate = this.weaponDictionary.attribute[weaponType]["fireRate"];
        this.initAmmo = this.weaponDictionary.attribute[weaponType]["initAmmo"];
        this.ammo = this.initAmmo;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
