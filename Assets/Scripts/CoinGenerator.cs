using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    private int amountOfCoins;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int minCoins;
    [SerializeField] private int maxCoins;
    [SerializeField] private SpriteRenderer[] coinImg;


    void Start()
    {
        for (int i = 0; i < coinImg.Length; i++)
        {
            coinImg[i].sprite = null;

        }
        amountOfCoins = Random.Range(maxCoins, minCoins); // coinleri random olarak değer veriyor
        int additionalOffset = amountOfCoins / 2;

        for (int i = 0; i < amountOfCoins; i++)
        {

            Vector3 offset = new Vector3(i - additionalOffset, 0); // oluşacak coinlerin konumlarını değiştirdik hepsi aynı yerde olmayacak.

            Instantiate(coinPrefab, transform.position, Quaternion.identity, transform); // sondaki transform ile oluşacak bütün coinleri coin generatorun childı yapmış olduk.





        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
