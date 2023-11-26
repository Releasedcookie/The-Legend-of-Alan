using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rbody;

    private bool isPlayerDead = false;


    [SerializeField] private AudioSource deathSoundEffect;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();

        if (doesPlayerHaveLives() == false)
        {
            rbody.bodyType = RigidbodyType2D.Static;
        }
        else
        {
            rbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") && isPlayerDead == false)
        {
            isPlayerDead = true;
            deathSoundEffect.Play();
            Die();
        }
    }

    private void Die()
    {
        anim.SetTrigger("death");
        FindFirstObjectByType<LivesSystem>().LoseALife();
        rbody.bodyType = RigidbodyType2D.Static;

    }

    // Restarts the level, this is called in the Death Anim
    public void restartLevel()
    {
        if (FindFirstObjectByType<LivesSystem>().DoesPlayerHaveLife())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private bool doesPlayerHaveLives()
    {
        return FindFirstObjectByType<LivesSystem>().DoesPlayerHaveLife();
    }

}
