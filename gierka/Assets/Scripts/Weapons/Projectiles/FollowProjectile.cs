using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowProjectile : MonoBehaviour
{
    public GameObject owner;

    // Update is called once per frame
    void Update()
    {
        if(owner != null)
            transform.position = owner.transform.position;
    }
}
