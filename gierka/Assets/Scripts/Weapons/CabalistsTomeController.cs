using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabalistsTomeController : WeaponBase
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    protected Animator m_Animator;
    protected float m_AnimationChangeDelay;
    protected float m_TimeSinceLastAnimationChange = 0.0f;

    void Start()
    {
        orbitRadius = 1.0f;
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(m_TimeSinceLastAnimationChange > m_AnimationChangeDelay)
        {
            m_AnimationChangeDelay = Random.Range(2, 4);
            m_Animator.SetFloat("AnimNum",Random.Range(1,3));
            m_Animator.SetTrigger("PlayIdleAnimation");
            m_TimeSinceLastAnimationChange = 0;
        }
        m_TimeSinceLastAnimationChange += Time.deltaTime;
    }
    public override void Attack()
    {
        Debug.Log("pew!");
        
        GameObject projectileObject = Instantiate(projectilePrefab, firePoint.transform.position, Quaternion.identity);

        BasicProjectile projectile = projectileObject.GetComponent<BasicProjectile>();
        projectile.Fire(m_AttackDirection, 300);
        
    }
}
