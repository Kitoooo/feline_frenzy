using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssalCurseController : Weapon
{
    [SerializeField]
    [Min(2)]
    protected int m_MaxTentacles;
    protected bool m_isShooting;

    protected List<AbyssalCurseTentacle> m_Tentacles;
    protected int currentTentacleIndex;

    protected override void Start()
    {
        base.Start();
        m_Tentacles = new List<AbyssalCurseTentacle>();
        int step = 180 / (m_MaxTentacles-1);
        for (int i = 0; i < m_MaxTentacles; i++)
        {
            GameObject projectileObject = Instantiate(projectilePrefab, firePoint.transform.position, transform.rotation);

            AbyssalCurseTentacle projectile = (AbyssalCurseTentacle)projectileObject.GetComponent<Projectile>();
            projectile.angle = step * i-90;
            projectile.OwningWeapon = this;
            m_Tentacles.Add(projectile);
        }
        currentTentacleIndex = 0;
    }
    protected override void Update()
    {
        base.Update();
        if (attacked)
            ExtendTentacles();
        else
            RetractTentacles();
    }

    public override void Attack()
    {
        //m_isShooting = true;
        foreach(var tentacle in m_Tentacles)
        {
            tentacle.allowForDamageTick = true;
        }
    }


    protected void ExtendTentacles()
    {
        if (currentTentacleIndex >= m_Tentacles.Count)
            return;

        AbyssalCurseTentacle current = m_Tentacles[currentTentacleIndex];
        if (current.isRetracted)
            current.Extend();
        else if(current.isExtended && currentTentacleIndex < m_Tentacles.Count-1)
            currentTentacleIndex += 1;
    }
    protected void RetractTentacles()
    {
        if (currentTentacleIndex < 0) 
            return;

        AbyssalCurseTentacle current = m_Tentacles[currentTentacleIndex];
        if (current.isExtended)
            current.Retract();
        else if (current.isRetracted && currentTentacleIndex > 0)
            currentTentacleIndex -= 1;
    }
}
