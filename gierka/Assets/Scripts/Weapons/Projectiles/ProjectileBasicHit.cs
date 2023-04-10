using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileBasicHit : ProjectileContact
{
    public override void OnContact(Projectile self, Collision2D other) 
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Health>().UpdateHealth(-self.OwningWeapon.attackDamage);
            CreateDamageIndicator(self, other.transform);
        }
        //Destroy(self.gameObject);
        Destroy(gameObject);
        self.markForDestroyFlags["BasicHit"] = true;

       
    }
}
//https://www.youtube.com/watch?v=mbX4FbDhx30