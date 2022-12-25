using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileBasicHit : ProjectileContact
{
    public override void OnContact(Projectile self, Collider2D other) 
    {
        if (other.gameObject.tag == "Enemy")
            other.gameObject.GetComponent<Enemy>().UpdateHealth(-self.damage);
        Destroy(self.gameObject);
        Destroy(gameObject);
    }
}
//https://www.youtube.com/watch?v=mbX4FbDhx30