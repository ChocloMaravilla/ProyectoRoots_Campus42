using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entities : MonoBehaviour
{
    public int coordX,coordY;
    public Direction direction;
    public GameObject raiz;
    public List<Transform> raices;
    void Start()
    {
        
    }
}
public enum Direction
{
    up,down,left,right
}