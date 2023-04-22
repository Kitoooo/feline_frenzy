using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Plays random animation from a set in a blend tree
//idle animation can not be canceled mid playing
public class IdleAnimationController : MonoBehaviour
{
    [SerializeField]
    protected float m_MinTimeBetweenAnimations;
    [SerializeField]
    protected float m_MaxTimeBetweenAnimations;
    [SerializeField]
    protected uint m_NumberOfAnimations;
    [SerializeField]
    protected string triggerName = "PlayIdleAnimation";
    [SerializeField]
    protected string AnimatorFloatName = "AnimationNumber";
    protected float m_AnimationChangeDelay;
    protected float m_TimeSinceLastAnimationChange = 0.0f;
    [SerializeField]
    protected Animator m_OnwerAnimator;

    void Update()
    {
        Animate();
    }

    private void Animate()
    {
        if (m_TimeSinceLastAnimationChange > m_AnimationChangeDelay)
        {
            m_AnimationChangeDelay = Random.Range(m_MinTimeBetweenAnimations, m_MaxTimeBetweenAnimations);
            m_OnwerAnimator.SetFloat(AnimatorFloatName, Random.Range(1, m_NumberOfAnimations));
            m_OnwerAnimator.SetTrigger(triggerName);
            m_TimeSinceLastAnimationChange = 0;
        }
        m_TimeSinceLastAnimationChange += Time.deltaTime;
    }
}
