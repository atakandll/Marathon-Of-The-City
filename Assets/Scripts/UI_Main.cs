using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Main : MonoBehaviour
{
    private bool gamePaused;
    private bool gameMuted;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject endGame;

    [Space]
    [SerializeField] private TextMeshProUGUI lastScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI coinText;

    [Header("Volume info")]
    [SerializeField] private UI_VolumeSlider[] slider;
    [SerializeField] private Image muteIcon;
    [SerializeField] private Image inGameMuteIcon;





    private void Start()
    {
        for (int i = 0; i < slider.Length; i++)
        {
            slider[i].SetupSlider(); // main menuye de gidilince kaydedilen verileri gösterirken son save halini gösteriyor buyüzden uı mainde yaptık.
        }
        SwitchMenu(mainMenu);



        lastScoreText.text = "Last Score:  " + PlayerPrefs.GetFloat("LastScore").ToString("#,#");

        highScoreText.text = "High Score:  " + PlayerPrefs.GetFloat("HighScore").ToString("#,#");


    }


    public void SwitchMenu(GameObject uiMenu) // menuler arasında gidiş geliş yapmamızı sağlayan metot.
    {
        for (int i = 0; i < transform.childCount; i++) // the number of children the parent transform has.
        {
            transform.GetChild(i).gameObject.SetActive(false);


        }
        uiMenu.SetActive(true);
        AudioManager.instance.PlaySFX(4);

        coinText.text = PlayerPrefs.GetInt("Coins").ToString("#,#");

    }
    public void SwitchSkyBox(int index)
    {
        AudioManager.instance.PlaySFX(4);
        GameManager.instance.SetupSkyBox(index);


    }

    public void MuteButton()
    {

        gameMuted = !gameMuted; //works like a switcher

        if (gameMuted)
        {
            muteIcon.color = new Color(1, 1, 1, .5f); // mute yapınca transparent yaptı.
            AudioListener.volume = 0;

        }
        else
        {
            muteIcon.color = Color.white;
            AudioListener.volume = 1;

        }
    }

    public void StartGameButton()
    {
        muteIcon = inGameMuteIcon;

        if (gameMuted)
        {
            muteIcon.color = new Color(1, 1, 1, .5f);
        }

        GameManager.instance.UnlockPlayer();

    }
    public void PauseGameButton()
    {
        if (gamePaused)
        {

            Time.timeScale = 1;
            gamePaused = false;
        }
        else // eğer pause yoksa şimdi pause edeceğiz.
        {

            Time.timeScale = 0;
            gamePaused = true;
        }




    }
    public void RestartGameButton() => GameManager.instance.RestartLevel();

    public void OpenEndGameUI()
    {
        SwitchMenu(endGame);
    }

}
