using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneCometProjectile : Projectile
{
    protected float m_CurrentAngle = 0;

    void Start()
    {
        m_CurrentAngle = OwningWeapon.angleToOwner * Mathf.Deg2Rad;
        markForDestroyFlags.Add("arcane-comet",false);
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        //base.Update();
        ArcaneCometController owner = (ArcaneCometController)OwningWeapon;
        speed = owner.currentProjectileSpeed;
        m_CurrentAngle += owner.currentProjectileSpeed * Mathf.Deg2Rad;
        Vector2 pos = new Vector3(Mathf.Cos(m_CurrentAngle) * owner.currentProjectileOrbit, Mathf.Sin(m_CurrentAngle) * owner.currentProjectileOrbit);
        transform.position = OwningWeapon.Owner.m_Body.position + pos + new Vector2(0, OwningWeapon.Owner.m_Renderer.bounds.size.y / 4);
    }
}
