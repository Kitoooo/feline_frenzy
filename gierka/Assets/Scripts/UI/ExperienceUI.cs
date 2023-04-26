using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceUI : MonoBehaviour
{
    public static ExperienceUI instance { get; private set; }
    private float m_CurrentExpLimit;
    protected float smoothing = 1;
    public Image mask;
    float originalSize;
    bool m_animating = false;
    float m_AnimationStartingSize;
    float m_ElapsedTime=0f;
    bool m_ExpOverflow = false;
    float m_OverflowValue;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
        //mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }

    public void SetValue(float value)
    {
        float newSize = originalSize * value;
        if (newSize >= m_CurrentExpLimit)
        {
            m_CurrentExpLimit = originalSize * value;
        }
        else
        {
            print("exp overflow");
            m_ExpOverflow = true;
            m_AnimationStartingSize = mask.rectTransform.rect.width;
            m_CurrentExpLimit = originalSize;
            m_OverflowValue = originalSize*value;
            StartCoroutine(SmoothIncrease());
        }

        if (!m_animating)
        {
            m_AnimationStartingSize = mask.rectTransform.rect.width;
            StartCoroutine(SmoothIncrease());
        }
    }

    private IEnumerator SmoothIncrease()
    {
        m_animating = true;
        m_ElapsedTime = 0;
        while ((int)mask.rectTransform.rect.width < (int)m_CurrentExpLimit)
        {
            m_ElapsedTime += Time.deltaTime;
            float value = Mathf.Lerp(m_AnimationStartingSize, m_CurrentExpLimit, m_ElapsedTime);
            mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value);
            yield return null;
        }
        
        if(m_ExpOverflow)
        {
            m_ExpOverflow = false;
            m_AnimationStartingSize = 0;
            m_CurrentExpLimit = m_OverflowValue;
            mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
            StartCoroutine(SmoothIncrease());
            yield return null;
        }
        m_animating = false;
        yield return null;
    }
}
