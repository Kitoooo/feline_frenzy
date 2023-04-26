using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileExplosion : ProjectileContact
{
    [SerializeField]
    protected float m_ExplosionRadius = 1.0f;
    [SerializeField]
    protected LayerMask m_LayerToCheck;
    protected bool m_Exploded = false;
    protected Animator m_Animator;

    private void Awake()
    {
        /*
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.transform.localScale = new Vector3(m_ExplosionRadius, m_ExplosionRadius, 1);
        m_ExplosionRadius *= 1.4142135623731f; //sqrt(2)
        m_Animator = GetComponent<Animator>();
        */
    }

    void Update()
    {
        /*if(m_Exploded)
        {
            if(m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                Destroy(gameObject);
            }
        }*/
    }

    public override void OnContact(Projectile self, Collision2D other)
    {
        Collider2D[] ObjectsInExplosion = Physics2D.OverlapCircleAll(transform.position, m_ExplosionRadius, m_LayerToCheck);
        foreach (Collider2D obj in ObjectsInExplosion)
        {
            if (obj.gameObject.tag == "Enemy")
            {
                self.DealDamageTo(obj.gameObject);
            }
        }
        
        self.markForDestroyFlags["BasicHit"] = true;
        m_Exploded = true;
        Destroy(gameObject);
    }
}
