using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LivesRegen : MonoBehaviour
{
    [SerializeField] private double LivesRespawnAfter_Seconds = 60;
    private double lifeRegainTimer = 0f;
    private int Lives;

    // For Lives Regen
    private DateTime timeNow;
    private DateTime DateQuit;
    private string lifeRegainTimerString;

    // Start is called before the first frame update
    void Start()
    {
        isNewPlayerCheck();
        getOfflineLivesRegen();
    }

    // Update is called once per frame
    void Update()
    {
        Lives = PlayerPrefs.GetInt("Lives");
        regenerateLife();
    }


    private void regenerateLife()
    {
        lifeRegainTimer += Time.deltaTime;

        if (lifeRegainTimer > LivesRespawnAfter_Seconds)
        {
            lifeRegainTimer -= LivesRespawnAfter_Seconds;
            if (Lives < 5)
            {
                Debug.Log("Regen Lives");
                Lives += 1;
            }
        }

        PlayerPrefs.SetInt("Lives", Lives);
    }

    private void isNewPlayerCheck()
    {
        if (!PlayerPrefs.HasKey("Lives"))
        {
            PlayerPrefs.SetInt("Lives", 5);
            Lives = 5;
        }
        else
        {
            Lives = PlayerPrefs.GetInt("Lives");
        }
    }

    public int timeTilNextLife()
    {
        int TimeTilNextLife;

        if (Lives < 5)
        {
            TimeTilNextLife = (int)(LivesRespawnAfter_Seconds - lifeRegainTimer);
        }
        else
        {
            TimeTilNextLife = -1;
        }

        return (int)TimeTilNextLife;
    }

    private void sendLivesRegainOnOffNotification()
    {
        double TotalLivesTimer = LivesRespawnAfter_Seconds * 5;
        double TimeTilFull = TotalLivesTimer - (LivesRespawnAfter_Seconds * Lives) - lifeRegainTimer;

        FindFirstObjectByType<MobileNotifications>().sendLivesFullNotification(TimeTilFull);
    }

    private void OnApplicationPause(bool pauseStatus)
    { // This functions works on Android and NOT on PC
        if (pauseStatus) // If Pause == true
        {
            // Log Game Quit Date
            DateQuit = DateTime.Now;
            PlayerPrefs.SetString("dateQuit", DateQuit.ToString());
            PlayerPrefs.SetString("lifeRegainTimer", lifeRegainTimer.ToString());

            if (Lives < 5) // Send Notification When Lives are regrown
            {
                sendLivesRegainOnOffNotification();
            }

            PlayerPrefs.Save();
        }
    }

    private void OnApplicationQuit()
    { // This function DOES NOT WORK on Android and only works on PC Games
      // Log Game Quit Date
        DateQuit = DateTime.Now;
        PlayerPrefs.SetString("dateQuit", DateQuit.ToString());
        PlayerPrefs.SetString("lifeRegainTimer", lifeRegainTimer.ToString());

        if (Lives < 5) // Send Notification When Lives are regrown
        {
            sendLivesRegainOnOffNotification();
        }

        PlayerPrefs.Save();
    }

    private void getOfflineLivesRegen()
    {
        timeNow = DateTime.Now;
        Debug.Log("START APP: Time now is: " + timeNow);
        string dateQuitString = PlayerPrefs.GetString("dateQuit");
        lifeRegainTimerString = PlayerPrefs.GetString("lifeRegainTimer");
        Debug.Log("START APP: Date Quit: " + dateQuitString);
        Debug.Log("START APP: lifeRegainTimer is: " + lifeRegainTimerString);
        Debug.Log(PlayerPrefs.GetString("HelloThere"));
        lifeRegainTimer = double.Parse(lifeRegainTimerString);

        if (!dateQuitString.Equals(""))
        {
            Debug.Log("dateQuitString is not null");
            DateQuit = DateTime.Parse(dateQuitString);
            timeNow = DateTime.Now;

            if (timeNow > DateQuit)
            {
                TimeSpan timespan = timeNow - DateQuit;
                Debug.Log("User was away for " + timespan.TotalSeconds + " Seconds");
                lifeRegainTimer = lifeRegainTimer + timespan.TotalSeconds;
                Debug.Log("New LifeTimer Is" + lifeRegainTimer);

                PlayerPrefs.SetString("dateQuit", "");
                PlayerPrefs.SetString("lifeRegainTimer", "0");

            }

        }
    }

}
