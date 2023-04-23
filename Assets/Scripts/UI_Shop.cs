using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public struct ColorToSell
{
    public Color color;
    public int price;
}

public enum ColorType
{
    playerColor,
    platformColor
}

public class UI_Shop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI notifyText;
    [Space]

    [Header("Platform colors")]
    [SerializeField] private GameObject platformColorButton; // bu kısım platformun rengini ayarlamak için shop kısmından
    [SerializeField] private Transform platformColorParent; // parent olucak instantiatelerin
    [SerializeField] private Image platformDisplay;
    [SerializeField] private ColorToSell[] platformColors;

    [Header("Player colors")]
    [SerializeField] private GameObject playerColorButton;
    [SerializeField] private Transform playerColorParent;
    [SerializeField] private Image playerDisplay;
    [SerializeField] private ColorToSell[] playerColors;


    void Start()
    {
        coinsText.text = PlayerPrefs.GetInt("Coins").ToString("#,#");

        for (int i = 0; i < platformColors.Length; i++)
        {
            Color color = platformColors[i].color;

            int price = platformColors[i].price;

            GameObject newButton = Instantiate(platformColorButton, platformColorParent); // oluşturduk

            newButton.transform.GetChild(0).GetComponent<Image>().color = color; // ımage rengini colortosell de yaptığımız renklere atadık
            newButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = price.ToString("#,#");

            newButton.GetComponent<Button>().onClick.AddListener(() => PurchaseColor(color, price, ColorType.platformColor)); // click yapmayı ınspectorden değil direkt burdan yaptık
        }

        for (int i = 0; i < playerColors.Length; i++)
        {
            Color color = playerColors[i].color;
            int price = playerColors[i].price;

            GameObject newButton = Instantiate(playerColorButton, playerColorParent);

            newButton.transform.GetChild(0).GetComponent<Image>().color = color;
            newButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = price.ToString("#,#");    // price.ToString("#,#");;

            newButton.GetComponent<Button>().onClick.AddListener(() => PurchaseColor(color, price, ColorType.playerColor));
        }
    }

    public void PurchaseColor(Color color, int price, ColorType colorType)
    {
        AudioManager.instance.PlaySFX(4);

        if (EnoughMoney(price))
        {
            if (colorType == ColorType.platformColor)
            {
                GameManager.instance.platformColor = color;

                platformDisplay.color = color;
            }
            else if (colorType == ColorType.playerColor)
            {
                GameManager.instance.player.GetComponent<SpriteRenderer>().color = color;

                GameManager.instance.SaveColor(color.r, color.g, color.b);

                playerDisplay.color = color;
            }

            StartCoroutine(Notify("Purchased successful", 1));
        }
        else
            StartCoroutine(Notify("Not enough money!", 1));

    }

    private bool EnoughMoney(int price)
    {
        int myCoins = PlayerPrefs.GetInt("Coins");

        if (myCoins > price)
        {
            int newAmountOfCoins = myCoins - price;

            PlayerPrefs.SetInt("Coins", newAmountOfCoins);

            coinsText.text = PlayerPrefs.GetInt("Coins").ToString("#,#"); // we are going to update the info every time we purchase something.
            return true;
        }
        return false;

    }

    IEnumerator Notify(string text, float seconds)
    {
        notifyText.text = text;

        yield return new WaitForSeconds(seconds);

        notifyText.text = "Click to buy";
    }
}
