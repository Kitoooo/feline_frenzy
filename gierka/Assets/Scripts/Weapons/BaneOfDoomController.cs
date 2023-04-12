using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaneOfDoomController : Weapon
{
    protected new void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, firePoint.transform.position, transform.rotation);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.OwningWeapon = this;
        projectile.Fire(m_AttackDirection);
    }
}
