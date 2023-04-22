using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D m_Body { get; protected set; }
    public float range = 1000.0f;
    protected float m_DistanceTravelled = 0f;
    [SerializeField] 
    public float speed = 300;
    [SerializeField]
    protected List<GameObject> m_ProjectileContactBehaviourPrefabs;
    public Weapon OwningWeapon { get; set; }

    

    private string[] m_TagsToIgnore = { "Player","PlayerProjectile","Weapon" };
    public string[] TagsToIgnore { get { return m_TagsToIgnore; } }

    //allow contacts scripts to store data between hits
    public Dictionary<string, object> contactStorage { get; protected set; }

    //to prevent destruction on contact at least one script has to set its flag to false
    public Dictionary<string, bool> markForDestroyFlags { get; protected set; }

    protected Vector3 lastVelocity;

    protected virtual void Awake()
    {
        m_Body = GetComponent<Rigidbody2D>();
        contactStorage = new Dictionary<string, object>();
        markForDestroyFlags = new Dictionary<string, bool>();
    }

    protected virtual void Update()
    {
        m_DistanceTravelled += speed * Time.deltaTime;
        bool marked = true;
        foreach(var flag in markForDestroyFlags)
        {
            if(flag.Value == false)
            {
                marked = false;
                break;
            }
        }
        lastVelocity = m_Body.velocity;
        if (m_DistanceTravelled > range || (marked && markForDestroyFlags.Count > 0))
        {
            Destroy(gameObject);
        }
    }

    public void Fire(Vector2 direction)
    {
        m_Body.AddForce(direction * speed);
    }

    public void DealDamageTo(GameObject enemy)
    {
        float critChance = OwningWeapon.m_CriticalChance / 100;
        uint critTier = (uint) critChance;
        float chanceForNextTimer = critChance - (int)critChance;
        float roll = Random.value;
        if (chanceForNextTimer >= roll)
            critTier++;

        float finalDamage = OwningWeapon.attackDamage + OwningWeapon.attackDamage * OwningWeapon.m_CriticalMultiplier * critTier;
        enemy.GetComponent<Health>().UpdateHealth(-finalDamage);
        DamageIndicatorFactory.Instance.CreateDamageIndicator(finalDamage,critTier,enemy.transform);    
    }

    /*
     *
     * I DONT KNOW HOW TO DO IT BETTER SORRY
     *
     */
    private void OnCollisionEnter2D(Collision2D other)
    {
        CheckContact(other);
    }

    protected void CheckContact(Collision2D other)
    {
        if (m_TagsToIgnore.Contains(other.gameObject.tag))
        {
            return;
        }
        else
        {
            foreach (GameObject projectileContactBehaviourPrefab in m_ProjectileContactBehaviourPrefabs)
            {
                GameObject projectileContactBehaviour = Instantiate(projectileContactBehaviourPrefab, transform.position, Quaternion.identity);

                ProjectileContact contactBehaviour = projectileContactBehaviour.GetComponent<ProjectileContact>();
                contactBehaviour.lastVelocity = lastVelocity;
                contactBehaviour.OnContact(this, other);
            }

        }
    }
    protected void CheckContact(Collider2D other)
    {
        if (m_TagsToIgnore.Contains(other.gameObject.tag))
        {
            return;
        }
        else
        {
            foreach (GameObject projectileContactBehaviourPrefab in m_ProjectileContactBehaviourPrefabs)
            {
                GameObject projectileContactBehaviour = Instantiate(projectileContactBehaviourPrefab, transform.position, Quaternion.identity);

                ProjectileContact contactBehaviour = projectileContactBehaviour.GetComponent<ProjectileContact>();
                contactBehaviour.lastVelocity = lastVelocity;
                contactBehaviour.OnContact(this, other);
            }

        }
    }

}   
