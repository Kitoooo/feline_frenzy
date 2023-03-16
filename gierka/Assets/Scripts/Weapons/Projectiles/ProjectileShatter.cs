using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShatter : ProjectileContact
{
    [SerializeField]
    protected int m_ProjectileCount;
    [SerializeField]
    protected GameObject m_ProjectileShardPrefab;
    // Start is called before the first frame update
    void Start()
    {
        m_ProjectileCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnContact(Projectile self, Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
            other.gameObject.GetComponent<Enemy>().UpdateHealth(-self.OwningWeapon.attackDamage);

        float step;
        if (m_ProjectileCount == 0)
            step = 360;
        else
            step = 360 / m_ProjectileCount;

        for (int i=0;i<m_ProjectileCount;i++)
        {
            var rotation = Quaternion.Euler(0, 0, i * step);
            Vector3 direction = Quaternion.Euler(0, 0, i * step) * Vector3.right;
            
            GameObject projectileObject = Instantiate(m_ProjectileShardPrefab, self.m_Body.transform.position, Quaternion.Euler(0, 0, i * step + 90));

            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.OwningWeapon = self.OwningWeapon;
            projectile.Fire(direction);
            
        }

        Destroy(self.gameObject);
        Destroy(gameObject);
    }
}
