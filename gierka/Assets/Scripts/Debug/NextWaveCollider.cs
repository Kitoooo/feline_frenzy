using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWaveCollider : MonoBehaviour
{
    public WaveSpawner spawner;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            spawner.NextWave();
        }
    }

}