using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : EnemyHealth
{
    [SerializeField]
    protected GameObject SkeletonPrefab;

    protected override void onDestroy()
    {
        Instantiate(SkeletonPrefab, transform.position, transform.rotation);
    }
}
