using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneCometController : Weapon
{
    [SerializeField]
    protected float m_MinProjectileOrbit;
    [SerializeField]
    protected float m_MaxProjectileOrbit;
    [SerializeField]
    protected float m_MinProjectileSpeed;
    [SerializeField]
    protected float m_MaxProjectileSpeed;

    public float currentProjectileOrbit { get; protected set; }
    public float currentProjectileSpeed { get; protected set; }


    //why am i still copy pasting it melee weapon are dead and buried
    public override void Attack()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, firePoint.transform.position, transform.rotation);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.OwningWeapon = this;
        //projectile.Fire(m_AttackDirection);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        Vector2 ownerPosition = m_Owner.m_Body.position;
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        currentProjectileOrbit = (mousePosition - ownerPosition).magnitude;
        currentProjectileOrbit = Mathf.Max(Mathf.Min(currentProjectileOrbit, m_MaxProjectileOrbit), m_MinProjectileOrbit);

        float ratio = Mathf.Lerp(m_MinProjectileSpeed,m_MaxProjectileSpeed, Mathf.InverseLerp(m_MinProjectileOrbit,m_MaxProjectileOrbit,currentProjectileOrbit));
        currentProjectileSpeed = Mathf.Lerp(m_MinProjectileSpeed, m_MaxProjectileSpeed, ratio);

    }
}
