using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;

    [SerializeField] private float parallaxEffect;

    private float length; // oluşturduğumuz spriteların uzunluğu
    private float xPosition;

    void Start()
    {
        cam = GameObject.Find("Main Camera"); // main cameraya inspector harici ulaşma 

        length = GetComponent<SpriteRenderer>().bounds.size.x; // sprite uzunluğunu alıyoruz

        xPosition = transform.position.x; // backgroundların x pozisyonu


    }


    void Update()
    {
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);

        float distanceToMove = cam.transform.position.x * parallaxEffect; // hareket ettirdiğimiz kısım

        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);// backgroundun yeni poziyonu, backgrund layer move forward

        if (distanceMoved > xPosition + length)
        {
            xPosition = xPosition + length;
        }
    }
}
