using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Entities
{
    // Start is called before the first frame update
    void Start()
    {
        coordX = 1;
        coordY = 9;
        direction = Direction.left;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePadre();
    }

    
}
