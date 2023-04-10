using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script makes me sad
public class ProjectileBounce : ProjectileContact
{
    [SerializeField]
    protected int m_MaxBounces;
    protected int m_BouncesLeft;

    private void Awake()
    {
        m_BouncesLeft = m_MaxBounces;
    }

    public override void OnContact(Projectile self, Collision2D other)
    {
        int bouncesLeft = getRemainingBounces(self);
        if (bouncesLeft <= 0)
        {
            //Destroy(self.gameObject);
            self.markForDestroyFlags["Bounce"] = true;
        }
        else
        {
            self.markForDestroyFlags["Bounce"] = false;
            var speed = lastVelocity.magnitude;

            //Vector2 baka = other.ClosestPoint(self.transform.position);
            //var direction = Vector3.Reflect(lastVelocity.normalized, baka.normalized);
            var direction = Vector3.Reflect(lastVelocity.normalized, other.contacts[0].normal);

            //rotate projectile left or right
            /*
            float angle = Vector3.Angle(direction, Vector3.right) + 180;
            if (angle < 270)
                self.m_Body.transform.rotation = Quaternion.Euler(0, 180, 0);
            else
                self.m_Body.transform.rotation = Quaternion.Euler(0, 0, 0);
            */
            Rigidbody2D rigidbody = self.GetComponent<Rigidbody2D>();
            if(lastVelocity.magnitude == 0)
            {
                Destroy(self.gameObject);
                Destroy(gameObject);
                return;
            }
            rigidbody.velocity = direction * Mathf.Max(speed, 0.0f);
            //rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, speed);
        }
        /*
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Health>().UpdateHealth(-self.OwningWeapon.attackDamage);
            CreateDamageIndicator(self, other.transform);
        }
        */
        //:)
        self.contactStorage["bouncesLeft"] = bouncesLeft - 1;
        Destroy(gameObject);
    }

    //this is horrible refactor this
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
