using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix : MonoBehaviour
{
    public int[,] matrix = new int[10, 10]
    {{0,0,0,0,0,0,0,0,0,0},
    {0,0,1,1,1,1,0,0,0,0},
    {0,0,1,0,0,1,0,0,0,0},
    {0,0,1,0,0,1,1,1,0,0},
    {0,0,1,0,0,0,0,1,0,0},
    {0,0,1,0,0,0,0,1,0,0},
    {0,0,1,0,0,0,0,1,0,0},
    {0,0,1,1,1,1,1,1,0,0},
    {0,0,0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0,0,0}};
    // Start is called before the first frame update
    void Start()
    {
        PrintMap();
        RellenarZona();
        PrintMap();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public void RellenarZona()
    {
        int top=GetTop(1);
        int bottom=GetBottom(1);
       
        for (int y = 0; y < 10; y++)
        {
            bool startLine = false;
            for (int x= 0; x < 10; x++)
            {
                if (y>=top && y <=bottom && !startLine && matrix[x,y]==1)
                {
                    startLine = true;
                }else if (startLine && x<GetMaxX(y,1))
                {
                    matrix[x, y] = 1;
                }

            }
        }

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
