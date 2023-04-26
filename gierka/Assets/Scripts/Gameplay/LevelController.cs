using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour
{
    public static LevelController instance { get; private set; }
    [SerializeField]
    protected TextMeshProUGUI m_LevelIndicator;
    protected float m_CurrentExp = 0f;
    [SerializeField]
    protected float m_ExpPerLevel = 2000f;
    public float m_CurrentEnemyLevel { get; private set; }
    public float m_CurrentPlayerLevel { get; private set; }

    private void Start()
    {
        instance = this;
        m_LevelIndicator.text = m_CurrentPlayerLevel.ToString();
    }

    public void addExperience(float xp)
    {
        m_CurrentExp += xp;
        if(m_CurrentExp >= m_ExpPerLevel)
        {
            m_CurrentExp -= m_ExpPerLevel;
            m_CurrentPlayerLevel += 1;
            m_LevelIndicator.text = m_CurrentPlayerLevel.ToString();
        }

        float progress = m_CurrentExp / m_ExpPerLevel;
        ExperienceUI.instance.SetValue(progress);
    }



}
