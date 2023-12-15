using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LivesSystem : MonoBehaviour
{
    private int Lives;
    [SerializeField] private Text LivesText;
    public double lifeRegainTimer = 0f;

    // Life Popup Objects
    [SerializeField] private GameObject LifeScreenPopup;
    [SerializeField] private Text NextLifeInPopup;
    [SerializeField] private Text NumberOfLivesText;
    [SerializeField] private Button closePopupButton;
    private bool isPopUpOpen = false;


    private void Start()
    {
        LifeScreenPopup.SetActive(false);
        Lives = PlayerPrefs.GetInt("Lives");
    }

    private void Update()
    {

        Lives = PlayerPrefs.GetInt("Lives");

        // Update Text on screen
        if (LivesText.text != Lives.ToString())
        {
            LivesText.text = Lives.ToString();
        }

        // Popup Menu
        if ((Lives == 0 || isPopUpOpen) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            OpenPlayerLifePopup();
        }
        else if (isPopUpOpen == false)
        {
            LifeScreenPopup.SetActive(false);
        }

    }

    public void LoseALife()
    {
        Debug.Log("Player has died");
        Lives = Lives - 1;
        PlayerPrefs.SetInt("Lives", Lives);
        PlayerPrefs.SetString("dateQuit", "");
    }

    public bool DoesPlayerHaveLife()
    {
        Lives = PlayerPrefs.GetInt("Lives");

        if (Lives <= 0)
        {  
            return false; 
        }
        else
        {
            return true;
        }
    }

    public void OpenPlayerLifePopup()
    {
        isPopUpOpen = true;
        LifeScreenPopup.SetActive(true);
        NumberOfLivesText.text = ("You have " + Lives.ToString() + " Lives");

        if (Lives == 0)
        {
            closePopupButton.interactable = false;
        }
        else
        {
            closePopupButton.interactable = true;
        }

        if (Lives == 5)
        {
            NextLifeInPopup.text = "Press Continue to Play";
        }
        else
        {
            NextLifeInPopup.text = ("Next Life In: " + FindFirstObjectByType<LivesRegen>().timeTilNextLife());
        }

    }

    public void ClosePlayerLifePopup()
    {
        LifeScreenPopup.SetActive(false);
        isPopUpOpen = false;
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            FindFirstObjectByType<PlayerLife>().restartLevel();
        }
    }

    public void homeButton()
    {
        ClosePlayerLifePopup();
        SceneManager.LoadScene(0);
    }

    public void WatchAdButton()
    {
        FindFirstObjectByType<RewardAds>().ShowRewardedAd();
        // resetLives();
    }

    public void resetLives()
    {
        Lives = 5;
        PlayerPrefs.SetInt("Lives", 5);
    }
 

}
