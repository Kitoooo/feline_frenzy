using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClearDamageIndicator : MonoBehaviour
{
    [SerializeField]
    protected TextMeshPro text;

    void Start()
    {
       //text = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if(text.color.a <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
