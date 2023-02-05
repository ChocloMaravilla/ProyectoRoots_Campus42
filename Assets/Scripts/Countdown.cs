using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI text;
    float n = 4f;

    void Update()
    {
        n -= Time.deltaTime;

        int num = (int)n;
        text.text = num.ToString();

        if (n < 0)
        {
            //Iniciar Partida
            Destroy(gameObject);
        }
        
    }
}
