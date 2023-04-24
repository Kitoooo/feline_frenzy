using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindseekerProjectile : Projectile
{
    [SerializeField]
    protected GameObject m_VortexPrefab;
    [SerializeField]
    protected float m_VortexRadius;
    [SerializeField]
    protected float m_PushForce;
    [SerializeField]
    protected float m_VortexSpawnInterval;
    protected float m_VortexSpawnTimer;
    [SerializeField]
    protected LayerMask m_LayerToCheck;

    void Start()
    {
        m_VortexSpawnTimer = 0;
    }

    
    protected override void Update()
    {
        base.Update();
        m_VortexSpawnTimer -= Time.deltaTime;
        if (m_VortexSpawnTimer<=0)
        {
            GameObject projectileObject = Instantiate(m_VortexPrefab, transform.position, transform.rotation);
            Collider2D[] ObjectsInVortex = Physics2D.OverlapCircleAll(transform.position, m_VortexRadius, m_LayerToCheck);
            projectileObject.GetComponent<FollowProjectile>().owner = this.gameObject;
            foreach (Collider2D obj in ObjectsInVortex)
            {
                if (obj.gameObject.tag == "Enemy")
                {
                    DealDamageTo(obj.gameObject);
                    Vector2 direction = (obj.transform.position - transform.position).normalized;
                    obj.GetComponent<Rigidbody2D>().AddForce(direction * m_PushForce);
                }
            }
            m_VortexSpawnTimer = m_VortexSpawnInterval;
        }
    }
}
