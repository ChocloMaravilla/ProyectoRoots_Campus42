using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeed : MonoBehaviour
{
    //public GameObject pickupEffect;

    public float Multiplire = 1.25f;
    public float duration = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(Pickup(collision));
        }
    }

    IEnumerator Pickup(Collider2D player)
    {
        //Modificar 


        yield return new WaitForSeconds(duration);

        //volver a la normalidad
        
        Destroy(gameObject);
    }
}
