using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    private float health = 0.0f;
    [SerializeField] private float maxHealth = 100.0f;

    void Start()
    {
        health = maxHealth;
    }
    public void UpdateHealth(float amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        //TODO: deduwa xD;
        health = 0.0f;
        Debug.Log("You died");
    }
}
