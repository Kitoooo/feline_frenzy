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
    protected float m_FadeDuration = 2.0f;

    private void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.transform.localScale = new Vector3(m_ExplosionRadius, m_ExplosionRadius, 1);
        m_ExplosionRadius *= 1.4142135623731f; //sqrt(2)
    }

    void Update()
    {
        if(m_Exploded)
        {
            m_FadeDuration -= Time.deltaTime;
            if(m_FadeDuration <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public override void OnContact(Projectile self, Collider2D other)
    {
        Collider2D[] ObjectsInExplosion = Physics2D.OverlapCircleAll(transform.position, m_ExplosionRadius, m_LayerToCheck);
        foreach (Collider2D obj in ObjectsInExplosion)
        {
            obj.gameObject.GetComponent<Enemy>().UpdateHealth(-self.damage);
        }
        Destroy(self.gameObject);
        m_Exploded = true;
    }
}
