using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceOfNatureController : WeaponBase
{
    public GameObject projectilePrefab;
    
    public override GameObject AbstractProjectilePrefab{ get { return projectilePrefab; }}
    void Start()
    {
        orbitRadius = 1.0f;
    }

    protected new void Update()
    {
        base.Update();
    }
    public override void Attack()
    {
        GameObject projectileObject = Instantiate(projectilePrefab,  firePoint.transform.position, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.OwningWeapon = this;
        projectile.Fire(m_AttackDirection);
    }
}
