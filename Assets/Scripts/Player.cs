using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Entities
{
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePadre();
    }
    public override void UpdatePadre()
    {
        base.UpdatePadre();
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Direction.down;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Direction.up;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Direction.right;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Direction.left;
        }
        if (!IsValidDir(direction)) { direction = Direction.none; }
    }
}