using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    protected Rigidbody2D m_Body;
    public float range = 1000.0f;
    public float damage;

    void Awake()
    {
        m_Body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (transform.position.magnitude > range)
        {
            Destroy(gameObject);
        }
    }

    public void Fire(Vector2 direction, float force)
    {
        m_Body.AddForce(direction * force);
    }

    //check if not owner
    void OnCollisionEnter2D(Collision2D other)
    {
        //dont destroy projectiles if they hit each other, will still trigger on contact with player
        //if (other.collider.GetComponent<BasicProjectile>() != null)
        //   return;
        Debug.Log("Projectile Collision with " + other.gameObject);
        Destroy(gameObject);
    }
}
