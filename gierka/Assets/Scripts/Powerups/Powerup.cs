using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public PowerupEffect effect;

    [SerializeField]
    public float m_Duration = 1;
    protected float m_RemainingDuration = 1;
    protected bool m_EffectApplied = false;
    protected GameObject m_AffectedTarget;

    protected SpriteRenderer m_Renderer;
    protected CircleCollider2D m_CircleCollider;

    private void Start()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
        m_CircleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (m_EffectApplied)
        {
            m_RemainingDuration -= Time.deltaTime;
            if (m_RemainingDuration <= 0)
            {
                effect.RemoveEffect(m_AffectedTarget);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_AffectedTarget = other.gameObject;
            m_EffectApplied = true;
            m_RemainingDuration = m_Duration;
            effect.ApplyEffect(other.gameObject);
            m_Renderer.enabled = false;
            m_CircleCollider.enabled = false;
        }
    }
}
