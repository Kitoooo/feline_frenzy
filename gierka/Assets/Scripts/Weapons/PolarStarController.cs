using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarStarController : Weapon
{
    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }
    public override void Attack()
    {
        //we wont be doing melee so i should stop copy-pasting this thing to every weapon
        GameObject projectileObject = Instantiate(projectilePrefab, firePoint.transform.position, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.OwningWeapon = this;
        projectile.Fire(m_AttackDirection);
    }
}
