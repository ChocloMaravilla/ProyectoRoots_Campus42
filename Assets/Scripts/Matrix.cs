using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Matrix : MonoBehaviour
{
    public GameObject cube;
    public Player player;
    public int[,] matrix = new int[10, 10]
    {{0,0,0,0,0,0,0,0,1,0},
    {1,1,1,1,1,1,1,1,1,0},
    {0,1,1,1,1,1,1,1,1,0},
    {0,1,1,1,1,1,1,1,1,0},
    {0,1,1,1,1,1,1,1,1,0},
    {0,1,1,1,1,1,1,1,1,0},
    {0,1,1,1,1,1,1,1,1,0},
    {0,1,1,1,1,1,1,1,1,0},
    {0,1,1,1,1,1,1,1,1,1},
    {0,1,0,0,0,0,0,0,0,0}};
    // Start is called before the first frame update
    void Start()
    {
        InstantiateDebugMap();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void InstantiateDebugMap()
    {
        float lastX = 0;
        float lastY = 0;
        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                if (matrix[x,y]==1)
                {
                    Instantiate(cube,new Vector3(x,0,y),Quaternion.identity);
                }
            }
        }
    }
    public void PrintMap()
    {
        string linea = "";
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                linea = linea + matrix[i,j];
            }
            linea=linea + "\n";
        }
        print(linea);
    }
    public Transform[] RellenarZona(GameObject flor,int playerColor)
    {
        int top=GetTop(playerColor);
        int bottom=GetBottom(playerColor);
        List<Transform> transforms = new List<Transform>();
        for (int y = 0; y < 10; y++)
        {
            bool startLine = false;
            for (int x= 0; x < 10; x++)
            {
                if (y>=top && y <=bottom && !startLine && matrix[x,y]== playerColor)
                {
                    startLine = true;
                }else if (startLine && x<GetMaxX(y, playerColor) && player.GetFlower(x, y) == null && (matrix[x + 1, y] != 0 && matrix[x - 1, y] != 0 && matrix[x, y + 1] != 0 && matrix[x, y - 1] != 0))
                {
                    matrix[x, y] = playerColor;
                    transforms.Add(Instantiate(flor,new Vector3(y,0,x),Quaternion.identity).transform);
                    transforms[transforms.Count - 1].GetComponent<Flower>().x = x;
                    transforms[transforms.Count - 1].GetComponent<Flower>().y = y;
                }

            }
        }
        return transforms.ToArray();
    }
    public Transform[] RellenarZonaInverse(GameObject flor, int playerColor)
    {
        int top = GetTop(playerColor);
        int bottom = GetBottom(playerColor);
        List<Transform> transforms = new List<Transform>();
        for (int x = 0; x < 10; x++)
        {
            bool startLine = false;
            for (int y = 0; y < 10; y++)
            {
                if (y >= top && y <= bottom && !startLine && matrix[x, y] == playerColor)
                {
                    startLine = true;
                }
                else if (startLine && x < GetMaxX(y, playerColor) && player.GetFlower(x,y)==null && (matrix[x + 1, y] != 0 && matrix[x - 1, y] != 0 && matrix[x, y + 1] != 0 && matrix[x, y - 1] != 0))
                {
                    matrix[x, y] = playerColor;
                    transforms.Add(Instantiate(flor, new Vector3(y, 0, x), Quaternion.identity).transform);
                    transforms[transforms.Count - 1].GetComponent<Flower>().x = x;
                    transforms[transforms.Count - 1].GetComponent<Flower>().y = y;
                }

            }
        }
        return transforms.ToArray();
    }
    public int GetMaxX(int yPos,int player)
    {
        int mayor = -1;
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (x > mayor && matrix[x, y] == player && y==yPos)
                {
                    mayor = x;
                }
            }
        }
        return mayor;
    }
    public int GetTop(int player)
    {
        int menor = 11;
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (y<menor && matrix[x,y]==player)
                {
                    menor = y;
                }
            }
        }
        return menor;
    }
    public int GetBottom(int player)
    {
        int menor = -1;
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (y > menor && matrix[x, y] == player)
                {
                    menor = y;
                }
            }
        }
        return menor;
    }
}
