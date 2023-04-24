using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindseekerController : Weapon
{
    //i need to stop
    public override void Attack()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, firePoint.transform.position, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.OwningWeapon = this;
        projectile.Fire(m_AttackDirection);
    }
}
