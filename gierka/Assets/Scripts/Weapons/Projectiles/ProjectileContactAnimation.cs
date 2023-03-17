using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileContactAnimation : ProjectileContact
{
    protected Animator m_Animator;


    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            Destroy(gameObject);
        }
    }

    public override void OnContact(Projectile self, Collision2D other)
    {
        transform.rotation = self.transform.rotation;
    }
}
