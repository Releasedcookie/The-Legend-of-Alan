using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemCollector : MonoBehaviour
{

    [SerializeField] private AudioSource collectionSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Cherry"))
        {
            collectionSoundEffect.Play();

            Destroy(collision.gameObject);

            FindFirstObjectByType<ScoringScript>().updateScore(1);
        }
    }
}
