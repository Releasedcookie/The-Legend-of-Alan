using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishScript : MonoBehaviour
{
    private AudioSource finishSound;
    private bool levelCompleted = false;

    private void Start()
    {
        finishSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            finishSound.Play();
            levelCompleted = true;
            Invoke("CompleteLevel", 0.5f);
        }
    }

    IEnumerator nextLevelRoutine()
    {
        FindFirstObjectByType<ScoringScript>().SaveScore();
        FindFirstObjectByType<InterstitialAds>().ShowAd();
        PlayerPrefs.SetInt("PlayerLevel", SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        return null;
    }

    public void CompleteLevel()
    {
        FindFirstObjectByType<ScoringScript>().SaveScore();
        double Level = SceneManager.GetActiveScene().buildIndex;
        if (Level / 5 == 1)
        {
            FindFirstObjectByType<InterstitialAds>().ShowAd();
        }
        else
        {
            MoveToNextLevel();
        }

        PlayerPrefs.SetInt("PlayerLevel", SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void MoveToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
