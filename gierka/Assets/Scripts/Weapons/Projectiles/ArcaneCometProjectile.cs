using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneCometProjectile : Projectile
{

    protected float m_CurrentAngle = 0;
    //implement max speed and min speed and lerp depending on current orbit
    [SerializeField]
    protected float OrbitSpeed = 0;

    void Start()
    {
        m_CurrentAngle = OwningWeapon.angleToOwner * Mathf.Deg2Rad;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        m_CurrentAngle += OrbitSpeed * Mathf.Deg2Rad;
        Vector3 pos = new Vector3(Mathf.Cos(m_CurrentAngle) * OwningWeapon.orbitRadius, Mathf.Sin(m_CurrentAngle) * OwningWeapon.orbitRadius, 0);
        transform.position = OwningWeapon.Owner.transform.position + pos;
    }
}
