using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public Transform[] GetSequence(GameObject flor,int playerColor)
    {
        List<Transform> transforms = new List<Transform>();
        for (int i = 0; i < 10; i++)
        {
            List<int[]> listaInts = GetXSequenceAtY(i,playerColor);

            for (int j = 0; j < listaInts.Count; j++)
            {
                for (int z = listaInts[j][0]; z < listaInts[j][1]; z++)
                {
                    if (player.GetFlower(z,i)==null)
                    {
                        matrix[z, i] = playerColor;
                        transforms.Add(Instantiate(flor, new Vector3(i, 0, z), Quaternion.identity).transform);
                        transforms[transforms.Count - 1].GetComponent<Flower>().x = z;
                        transforms[transforms.Count - 1].GetComponent<Flower>().y = i;
                    }
                }
            }
        }
        return transforms.ToArray();
    }
    public List<int[]> GetXSequenceAtY(int y,int playerColor)
    {
        List<int[]> listaInts = new List<int[]>();
        bool finished = false;
        int nextStop = 0;
        while (!finished)
        {
            int[] ar = SeqX(y, nextStop, playerColor);
            if (ar[1]!=-1 && ar[0]!=-1)
            {
                listaInts.Add(ar);
                int temp = ar[1];
                bool init = false;
                int type = -1;
                if (matrix[ar[1]+1, y] == playerColor && !init)
                {
                    type = 0;
                }
                else if (matrix[ar[1] + 1, y] != playerColor && !init)
                {
                    type = 2;
                }
                switch (type)
                {
                    case 0:
                        for (int i = ar[1] + 1; i < 10; i++)
                        {
                            if (matrix[i, y] != playerColor || i >= 9)
                            {
                                temp= i-1;
                                break;
                            }
                        }
                        break;
                    case 2:
                        for (int i = ar[1] + 1; i < 10; i++)
                        {
                            if (matrix[i, y] == playerColor)
                            {
                                temp= i;
                                break;
                            }
                            else if (i >= 9)
                            {
                                temp= - 1;
                            }
                        }
                        break;
                }
                nextStop = temp;
            }else{
                finished = true;
            }
            
        }
        return listaInts;
    }
    public int[] SeqX(int y,int startX,int player)
    {
        int[] ints= new int[2];
        ints[0] = -1;
        ints[1] = -1;
        bool init = false;
        if (startX!=-1)
        {
            for (int i = startX; i < 10; i++)
            {
                if (!init && matrix[i, y] == player)
                {
                    ints[0] = i;
                    init = true;
                }
            }
            if (ints[0] != -1)
            {
                ints[1] = NextX(ints[0], y, player);
            }
        }
        
        
        return ints;
    }
    public int NextX(int start,int y,int player)
    {
        bool init = false;
        int type = -1;
        if (matrix[start+1, y] == player && !init)
        {
            type = 0;
        }
        else if (matrix[start+1, y] != player && !init)
        {
            type = 2;
        }
        switch (type)
        {
            case 0:
                for (int i = start+1; i < 10; i++)
                {
                    if (matrix[i,y]!=player || i>=9)
                    {
                        return i-1;
                    }
                }
                break;
            case 2:
                for (int i = start+1; i < 10; i++)
                {
                    if (matrix[i, y] == player)
                    {
                        return i;
                    }else if (i>=9)
                    {
                        return -1;
                    }
                }
                break;
        }
        return -1;
    }
    public Transform[] RellenarZona(GameObject flor,int playerColor)
    {
        int top=GetTop(playerColor);
        int bottom=GetBottom(playerColor);
        List<Transform> transforms = new List<Transform>();
        for (int y = 0; y < 10; y++)
        {
            int par = 0;
            for (int x= 0; x < 10; x++)
            {
                if (y>=top && y <=bottom && matrix[x + 1, y] != playerColor && matrix[x,y]== playerColor)
                {
                    par++;
                }
                else if (par%2!=0 && x<GetMaxX(y, playerColor) && player.GetFlower(x, y) == null && (matrix[x + 1, y] != 0 && matrix[x - 1, y] != 0 && matrix[x, y + 1] != 0 && matrix[x, y - 1] != 0))
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
                    if (matrix[x, y+1] != playerColor)
                    {
                        startLine = false;
                    }
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