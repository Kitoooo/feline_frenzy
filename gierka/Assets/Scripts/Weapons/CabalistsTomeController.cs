using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabalistsTomeController : Weapon
{
    protected override void Start()
    {
        base.Start();
        orbitRadius = 1.0f;
    }

    protected new void Update()
    {
        base.Update();
    }
    public override void Attack()
    {
        // Debug.Log("pew!");

        GameObject projectileObject = Instantiate(projectilePrefab, firePoint.transform.position, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.OwningWeapon = this;
        projectile.Fire(m_AttackDirection);

    }
}
