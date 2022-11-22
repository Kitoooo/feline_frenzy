using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    protected Rigidbody2D m_Body;
    public float range = 1000.0f;
    [SerializeField] public float damage = 10f;
    [SerializeField] public float speed = 300;

    private string[] m_TagsToIgnore = { "Player","PlayerProjectile","Weapon" };
    


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

    public void Fire(Vector2 direction)
    {
        m_Body.AddForce(direction * speed);
    }

    //check if not owner
    void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.Log("Collision with: "+ other.gameObject.tag);
        if(m_TagsToIgnore.Contains(other.gameObject.tag))
        {
            return;
        }
        else if (other.gameObject.tag == "Enemy"){
            other.gameObject.GetComponent<Enemy>().UpdateHealth(-damage);
        }

        Destroy(gameObject);

    }
}
