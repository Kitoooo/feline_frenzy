using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class ProjectileContact : MonoBehaviour
{
    public Vector2 lastVelocity { get; set; }

    [SerializeField]
    protected static GameObject m_DamageIndicatorPrefab;
    public virtual void OnContact(Projectile self, Collision2D other)
    {
        throw new System.NotImplementedException();
    }
    public virtual void OnContact(Projectile self, Collider2D other)
    {
        throw new System.NotImplementedException();
    }
}
