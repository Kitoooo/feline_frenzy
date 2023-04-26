using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    protected float m_CurrentHealth = 0.0f;
    [SerializeField]
    protected float m_MaxHealth = 100.0f;
    public bool isDead { get; protected set; }

    void Awake()
    {
        m_CurrentHealth = m_MaxHealth;
        isDead = false;
    }
    public void UpdateHealth(float amount)
    {
        //Debug.Log("taking damage" + amount);
        m_CurrentHealth += amount;
        m_CurrentHealth = Mathf.Min(m_CurrentHealth, m_MaxHealth);
        if(amount < 0.0f)
        {
            onDamageTaken();
        }
        if(m_CurrentHealth <= 0.0f)
        {
            isDead = true;
            onDeath();
        }
    }

    protected abstract void onDeath();
    protected abstract void onDamageTaken();
}
