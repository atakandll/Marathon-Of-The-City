using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] protected float chanceToSpawn = 1;
    protected virtual void Start()
    {
        bool canSpawn = chanceToSpawn >= Random.Range(0, 2);

        if (!canSpawn)
        {
            Destroy(gameObject);
        }

    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            other.GetComponent<Player>().Damage();


        }

    }
}
