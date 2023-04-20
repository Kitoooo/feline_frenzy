using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileBasicHit : ProjectileContact
{
    public override void OnContact(Projectile self, Collision2D other) 
    {
        if (other.gameObject.tag == "Enemy")
            self.DealDamageTo(other.gameObject);

        //Destroy(self.gameObject);
        Destroy(gameObject);
        self.markForDestroyFlags["BasicHit"] = true;
    }
    public override void OnContact(Projectile self, Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
            self.DealDamageTo(other.gameObject);

        //Destroy(self.gameObject);
        Destroy(gameObject);
        self.markForDestroyFlags["BasicHit"] = true;
    }
}
//https://www.youtube.com/watch?v=mbX4FbDhx30