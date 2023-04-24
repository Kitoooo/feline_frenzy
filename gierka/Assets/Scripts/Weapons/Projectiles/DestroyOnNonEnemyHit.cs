using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnNonEnemyHit : ProjectileContact
{
    public override void OnContact(Projectile self, Collision2D other)
    {
        if (other.gameObject.tag != "Enemy")
            self.markForDestroyFlags["NonEnemyHit"] = true;

        Destroy(gameObject); 
    }

    public override void OnContact(Projectile self, Collider2D other)
    {
        if (other.gameObject.tag != "Enemy")
            self.markForDestroyFlags["NonEnemyHit"] = true;

        Destroy(gameObject);
    }
}
