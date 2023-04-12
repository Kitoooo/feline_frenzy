using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssalCurseTentacle : Projectile
{
    protected Animator m_Animator;
    //should be protected, tough luck
    [HideInInspector]
    public bool isRetracted;
    [HideInInspector]
    public bool isExtended;
    [HideInInspector]
    public float angle = 0;
    [HideInInspector]
    public bool allowForDamageTick=false;


    protected override void Awake()
    {
        base.Awake();
        markForDestroyFlags.Add("tentacle",false);
        m_Animator = GetComponent<Animator>();
        isRetracted = true;
        isExtended = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        //base.Update();
        gameObject.transform.position = OwningWeapon.firePoint.transform.position;
        gameObject.transform.rotation = OwningWeapon.firePoint.transform.rotation;
        gameObject.transform.Rotate(Vector3.forward,angle);
    }

    public void Extend()
    {
        // _isFullyRetracted = false;
        //set collinder to max size in animation script
        //https://answers.unity.com/questions/1306123/do-something-after-animation-finishes.html
        m_Animator.SetBool("isShooting", true);
    }

    public void Retract()
    {
        //_isFullyExtented = false;
        //set collinder to 0 size in animation script
        //https://answers.unity.com/questions/1306123/do-something-after-animation-finishes.html
        m_Animator.SetBool("isShooting", false);
    }
    protected void OnTriggerStay2D(Collider2D collision)
    {
        if(allowForDamageTick)
        {
            CheckContact(collision);
            allowForDamageTick = false;
        }
    }
}
