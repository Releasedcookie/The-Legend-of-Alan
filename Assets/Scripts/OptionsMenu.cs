using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private Button MusicMuteButton;
    [SerializeField] private AudioSource musicControl;

    // Start is called before the first frame update
    void Start()
    {
        optionsMenu.SetActive(false);
    }

    public void OptionsMenu_OpenClose()
    {
        if (optionsMenu.activeSelf) {
            optionsMenu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            optionsMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void homeButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void turnMusicUp()
    {
        musicControl.volume = musicControl.volume + 0.1f;
        MusicMuteButton.interactable = true;
        musicControl.mute = false;
    }
    public void turnMusicDown()
    {
        musicControl.volume = musicControl.volume - 0.1f;
        MusicMuteButton.interactable = true;
        musicControl.mute = false;
    }
    public void MusicMute()
    {
        musicControl.mute = true;
        MusicMuteButton.interactable = false;
    }
    public void resetGame()
    {
        FindFirstObjectByType<StartMenu>().resetGame();
    }
}
