using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringScript : MonoBehaviour
{

    private int cherries = 0;
    [SerializeField] private Text cherriesText;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("CherryCount"))
        {
            cherries = 0;
            PlayerPrefs.SetInt("CherryCount", 0);
        }
        else
        {
            cherries = PlayerPrefs.GetInt("CherryCount");
        }
        cherriesText.text = "Cherries: " + cherries;
    }

    // Update is called once per frame
    public void updateScore(int addToScore)
    {
        cherries += addToScore;
        cherriesText.text = "Cherries: " + cherries;
    }
    public void SaveScore()
    {
        PlayerPrefs.SetInt("CherryCount", cherries);
    }
    public void resetScore()
    {
        cherries = 0;
        PlayerPrefs.SetInt("CherryCount", cherries);
        cherriesText.text = "Cherries: " + cherries;
    }
}
