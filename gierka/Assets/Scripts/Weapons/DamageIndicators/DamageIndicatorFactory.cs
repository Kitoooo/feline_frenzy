using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageIndicatorFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject DamageIndicatorPrefab;

    [SerializeField]
    protected Color BasicColor;
    [SerializeField]
    protected Color Tier1CritColor;
    [SerializeField]
    protected Color Tier2CritColor;
    [SerializeField]
    protected Color Tier3CritColor;
    [SerializeField]
    protected Color Above3CritColor;

    public static DamageIndicatorFactory Instance { get; private set; }

    public void Start()
    {
        Instance = this;
    }

    public void CreateDamageIndicator(float damage, uint tier, Transform transform)
    {
        GameObject indicator = Instantiate(DamageIndicatorPrefab, transform.position, Quaternion.identity);
        TextMeshPro text = indicator.GetComponentInChildren<TextMeshPro>();
        Animator animator = indicator.GetComponentInChildren<Animator>();
        animator.speed += Random.Range(-0.3f, 0.3f);
        text.text = damage.ToString();

        //use switch yes yes yes w/e
        if(tier > 0)
        {
            text.fontSize *= 1.3f;
            if (tier == 1)
                text.color = Tier1CritColor;
            else if (tier == 2)
                text.color = Tier2CritColor;
            else if (tier == 3)
                text.color = Tier3CritColor;
            else 
                text.color = Above3CritColor;
        }
    }

    
}