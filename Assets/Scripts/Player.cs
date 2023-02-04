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
        if (raices.Count==0)
        {
            CreateRaiz();
        }else if (raices[raices.Count-1].GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime>1)
        {
            Move();
            CreateRaiz();
        }
    }
    public void CreateRaiz()
    {
        switch (direction)
        {
            case Direction.up:
                raices.Add(Instantiate(raiz, new Vector3(coordY, 0, coordX), Quaternion.Euler(0,90,0)).transform);
                break;
            case Direction.down:
                raices.Add(Instantiate(raiz, new Vector3(coordY, 0, coordX), Quaternion.Euler(0, -90, 0)).transform);
                break;
            case Direction.left:
                raices.Add(Instantiate(raiz, new Vector3(coordY, 0, coordX), Quaternion.Euler(0, 0, 0)).transform);
                break;
            case Direction.right:
                raices.Add(Instantiate(raiz, new Vector3(coordY, 0, coordX), Quaternion.Euler(0, 180, 0)).transform);
                break;
            default:
                break;
        }
    }
    public void Move()
    {
        switch (direction)
        {
            case Direction.up:
                coordX--;

                break;
            case Direction.down:
                coordX++;
                break;
            case Direction.left:
                coordY--;
                break;
            case Direction.right:
                coordY++;
                break;
            default:
                break;
        }
    }
}
