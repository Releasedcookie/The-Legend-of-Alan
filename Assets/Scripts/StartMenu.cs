using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Text nextLifeIn;
    private int NextLifeTimer;
    [SerializeField] private GameObject player;
    private Rigidbody2D rbody;

    private int UserLevel;

    private void Start()
    {
        rbody = player.GetComponent<Rigidbody2D>();

        FindFirstObjectByType<LivesSystem>().ClosePlayerLifePopup();

        // Get Saved Game
        if (!PlayerPrefs.HasKey("PlayerLevel"))
        {
            PlayerPrefs.SetInt("PlayerLevel", 1);
        }
    }

    private void Update()
    {
        // FindFirstObjectByType<LivesSystem>().ClosePlayerLifePopup();
        //nextLifeIn_Int = FindFirstObjectByType<LivesSystem>().timeTilNextLife();
        NextLifeTimer = FindFirstObjectByType<LivesRegen>().timeTilNextLife();
        if (NextLifeTimer == -1)
        {
            nextLifeIn.text = "";
        }
        else
        {
            nextLifeIn.text = ("Next Life In: " + FindFirstObjectByType<LivesRegen>().timeTilNextLife());
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartGameRoutine());
    }
    public void resetGame()
    {
        PlayerPrefs.SetInt("PlayerLevel", 1);
        FindFirstObjectByType<ScoringScript>().resetScore();
        FindFirstObjectByType<LivesSystem>().resetLives();
    }

    IEnumerator StartGameRoutine()
    {
        UserLevel = PlayerPrefs.GetInt("PlayerLevel");
        rbody.gravityScale = 2;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(UserLevel);
    }
}
