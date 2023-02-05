using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entities : MonoBehaviour
{
    public int coordX,coordY;
    public Direction direction;
    public GameObject raiz;
    public GameObject flor;
    public List<Transform> raices;
    public List<Transform> flores;
    public List<Transform> floresHistory;
    public bool flower;
    public Matrix matriz;
    public void UpdatePadre()
    {
        if (raices.Count == 0)
        {
            CreateRaiz();
        }
        else if (raices[raices.Count - 1].GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {

            CreateRaiz();
            Move();
            flower = false;
        }
        if (raices.Count != 0 && raices[raices.Count - 1].GetChild(0).GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && !flower)
        {
            CreateFlower();
        }
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
    }
    public void CreateFlower()
    {
        if (IsThereAFlower())
        {
            print("hola");
            Transform[] trans = matriz.GetSequence(flor, 1);
            for (int i = 0; i < trans.Length; i++)
            {
                floresHistory.Add(trans[i]);
            }
            flores.Clear();
            flores = new List<Transform>();
        }
        else
        {
            GameObject newFlor = Instantiate(flor, new Vector3(coordY, 0, coordX), Quaternion.identity);
            flores.Add(newFlor.transform);
            flores[flores.Count - 1].GetComponent<Flower>().x = coordX;
            flores[flores.Count - 1].GetComponent<Flower>().y = coordY;
            floresHistory.Add(flores[flores.Count - 1]);
        }
        flower = true;
    }
    public bool CheckX()
    {
        for (int i = 0; i < flores.Count; i++)
        {
            int cuantity = 0;
            if (GetFlower(flores[i].GetComponent<Flower>().x, flores[i].GetComponent<Flower>().y)!=null
                && GetFlower(flores[i].GetComponent<Flower>().x, flores[i].GetComponent<Flower>().y+1) != null)
            {
                cuantity++;
            }
            if (GetFlower(flores[i].GetComponent<Flower>().x, flores[i].GetComponent<Flower>().y) != null
                && GetFlower(flores[i].GetComponent<Flower>().x, flores[i].GetComponent<Flower>().y-1) != null)
            {
                cuantity++;
            }
            if ((GetFlower(flores[i].GetComponent<Flower>().x-1, flores[i].GetComponent<Flower>().y) != null || GetFlower(flores[i].GetComponent<Flower>().x+1, flores[i].GetComponent<Flower>().y))
                && GetFlower(flores[i].GetComponent<Flower>().x, flores[i].GetComponent<Flower>().y) != null)
            {
                cuantity++;
            }
            if (cuantity==3)
            {
                return false;
            }
        }
        return true;
    }
    public Transform GetFlower(int x,int y)
    {
        for (int i = 0; i < floresHistory.Count; i++)
        {
            if (floresHistory[i].GetComponent<Flower>().x==x && floresHistory[i].GetComponent<Flower>().y == y)
            {
                return floresHistory[i];
            }
        }
        return null;
    }

    public bool IsThereAFlower()
    {
        for (int i = 0; i < flores.Count; i++)
        {
            for (int j = 0; j < floresHistory.Count; j++)
            {
                if ((flores[i].GetComponent<Flower>().y == coordY && flores[i].GetComponent<Flower>().x == coordX) || (floresHistory[j].GetComponent<Flower>().y == coordY && floresHistory[j].GetComponent<Flower>().x == coordX))
                {
                    return true;
                }
            }

        }
        return false;
    }
    public void CreateRaiz()
    {
        switch (direction)
        {
            case Direction.up:
                raices.Add(Instantiate(raiz, new Vector3(coordY, 0, coordX), Quaternion.Euler(0, -90, 0)).transform);
                break;
            case Direction.down:
                raices.Add(Instantiate(raiz, new Vector3(coordY, 0, coordX), Quaternion.Euler(0, 90, 0)).transform);
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
        matriz.matriz[coordX, coordY].owner=Owner.Blue;
    }
}
public enum Direction
{
    up,down,left,right
}