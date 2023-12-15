using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LivesRegen : MonoBehaviour
{
    [SerializeField] private float LivesRespawnAfter_Seconds = 120f;
    private float lifeRegainTimer = 0f;
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
        lifeRegainTimer = PlayerPrefs.GetFloat("lifeRegainTimer");

        Debug.Log(lifeRegainTimer);

        if (Lives < 5)
        {
            lifeRegainTimer += Time.deltaTime;
            PlayerPrefs.SetFloat("lifeRegainTimer", lifeRegainTimer);

            if (lifeRegainTimer >= LivesRespawnAfter_Seconds)
            {
                lifeRegainTimer = lifeRegainTimer - LivesRespawnAfter_Seconds;
                PlayerPrefs.SetFloat("lifeRegainTimer", lifeRegainTimer);
                regenerateLife();
            }
        }
        else
        {
            lifeRegainTimer = 0;
            PlayerPrefs.SetFloat("lifeRegainTimer", lifeRegainTimer);
        }
    }

    private void regenerateLife()
    {
        Debug.Log("Regen Lives");
        Lives += 1;
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
            Debug.Log("OnApplicationPause() - User is bye bye");

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
        Debug.Log("OnApplicationQuit() - User is bye bye");

        if (Lives < 5) // Send Notification When Lives are regrown
        {
            sendLivesRegainOnOffNotification();
        }

        PlayerPrefs.Save();
    }

    private void getOfflineLivesRegen()
    {
        timeNow = DateTime.Now;
        string dateQuitString = PlayerPrefs.GetString("dateQuit");
        float offlineRegenTimer = PlayerPrefs.GetFloat("lifeRegainTimer");

        //Debug.Log("START APP: Time now is: " + timeNow);
        //Debug.Log("START APP: Date Quit: " + dateQuitString);
        //Debug.Log("START APP: lifeRegainTimer is: " + offlineRegenTimer);

        lifeRegainTimer = offlineRegenTimer;

        if (!dateQuitString.Equals(""))
        {
            // Debug.Log("dateQuitString is not null");
            DateQuit = DateTime.Parse(dateQuitString);
            timeNow = DateTime.Now;

            if (timeNow > DateQuit)
            {
                TimeSpan timespan = timeNow - DateQuit;
                //Debug.Log("User was away for " + timespan.TotalSeconds + " Seconds");
                lifeRegainTimer = lifeRegainTimer + Convert.ToSingle(timespan.TotalSeconds);
                //Debug.Log("New LifeTimer Is" + lifeRegainTimer);

                PlayerPrefs.SetString("dateQuit", "");
                PlayerPrefs.SetFloat("lifeRegainTimer", lifeRegainTimer);

            }

        }
    }

}
