using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBounce : ProjectileContact
{
    [SerializeField]
    protected int m_MaxBounces;
    protected int m_BouncesLeft;

    private void Awake()
    {
        m_BouncesLeft = m_MaxBounces + 1;
    }

    public override void OnContact(Projectile self, Collider2D other)
    {
        int bouncesLeft = getRemainingBounces(self);
        if (bouncesLeft < 0)
        {
            Destroy(self.gameObject);
        }
        else
        {
            var speed = lastVelocity.magnitude;

            Vector2 baka = other.ClosestPoint(self.transform.position);
            var direction = Vector3.Reflect(lastVelocity.normalized, baka.normalized);
            //var direction = Vector3.Reflect(lastVelocity.normalized, other.contacts[0].normal);

            Rigidbody2D rigidbody = self.GetComponent<Rigidbody2D>();
            rigidbody.velocity = direction * Mathf.Max(speed, 10.0f);
        }

        if (other.gameObject.tag == "Enemy")
            other.gameObject.GetComponent<Enemy>().UpdateHealth(-self.OwningWeapon.attackDamage);
        //:)
        self.contactStorage["bouncesLeft"] = bouncesLeft - 1;
        Destroy(gameObject);
    }

    protected int getRemainingBounces(Projectile self)
    {
        object buffer;

        if (!self.contactStorage.TryGetValue("bouncesLeft", out buffer))
        {
            self.contactStorage.Add("bouncesLeft", m_MaxBounces);
            return m_MaxBounces;
        }

        int bouncesLeft = (int)buffer;
        return bouncesLeft;
    }
    //https://stackoverflow.com/questions/45782473/get-contactpoint2d-contact-normal-from-the-ontriggerxxx-function
}
