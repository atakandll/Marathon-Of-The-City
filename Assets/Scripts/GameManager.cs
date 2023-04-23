using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UI_Main ui;
    public Player player;
    public static GameManager instance;

    [Header("Skybox meterials")]
    [SerializeField] private Material[] skyBoxMat;


    [Header("Purchased color")]
    public Color platformColor;


    [Header("Score info")]
    public int coins;
    public float distance;
    public float score;


    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;
        SetupSkyBox(PlayerPrefs.GetInt("SkyBoxSettings"));

    }
    private void Start()
    {
        QualitySettings.vSyncCount = 0; // Sync the frame rate to the screen's refresh rate
        Application.targetFrameRate = 120; // Make the game run as fast as possible

    }
    public void SetupSkyBox(int i)
    {
        if (i <= 1)
        {
            RenderSettings.skybox = skyBoxMat[i]; // eğer 1 den küçükse 

        }
        else
        {
            RenderSettings.skybox = skyBoxMat[Random.Range(0, skyBoxMat.Length)]; // değilse random atıcak
        }
        PlayerPrefs.SetInt("SkyBoxSetting", i);

    }
    public void SaveColor(float r, float g, float b)
    {
        PlayerPrefs.SetFloat("ColorR", r); // renkleri kaydedicez ve default değer atadık
        PlayerPrefs.SetFloat("ColorG", g);
        PlayerPrefs.SetFloat("ColorB", b);
    }
    private void LoadColor()
    {
        SpriteRenderer sr = player.GetComponent<SpriteRenderer>();

        Color newColor = new Color(PlayerPrefs.GetFloat("ColorR"),
                                   PlayerPrefs.GetFloat("ColorG"),
                                   PlayerPrefs.GetFloat("ColorB"),
                                   PlayerPrefs.GetFloat("ColorA", 1));

        sr.color = newColor;


    }
    private void Update()
    {
        if (player.transform.position.x > distance)
        {
            distance = player.transform.position.x;

        }

    }
    public void UnlockPlayer() => player.playerUnlocked = true;
    public void RestartLevel()
    {

        SceneManager.LoadScene(0);
    }

    public void SaveInfo()
    {
        int savedCoins = PlayerPrefs.GetInt("Coins"); // we need to get the amount of coins saved already.

        PlayerPrefs.SetInt("Coins", savedCoins + coins);

        score = distance * coins;

        PlayerPrefs.SetFloat("LastScore", score);

        if (PlayerPrefs.GetFloat("HighScore") < score)
        {
            PlayerPrefs.SetFloat("HighScore", score);
        }






    }
    public void GameEnded()
    {
        SaveInfo();
        ui.OpenEndGameUI();
    }





}
