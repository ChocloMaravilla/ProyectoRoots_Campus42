using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Matrix1 : MonoBehaviour
{
    public GameObject cube;
    public Player player;
    public int[,] matrix;
    public Casilla[,] matriz=new Casilla[20,20];
    public BoardData data = new BoardData();
    // Start is called before the first frame update
    void Start()
    {
        data.LoadFromFile("11.json");
        matrix=data.GetRawTileMatrix();
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                matriz[x, y] = new Casilla();
                matriz[x, y].types = (Types)matrix[x, y];
            }
        }
        InstantiateDebugMap();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void InstantiateDebugMap()
    {
        for (int y = 0; y < 20; y++)
        {
            for (int x = 0; x < 20; x++)
            {
                switch (matriz[x,y].types)
                {
                    case Types.None:
                        break;
                    case Types.Spawn:
                        GameObject newCasilla=Instantiate(cube, new Vector3(x, 0, y), Quaternion.identity);
                        newCasilla.GetComponent<MeshRenderer>().material.color = Color.yellow;
                        break;
                    case Types.Basics:
                        GameObject newCasilla2 = Instantiate(cube, new Vector3(x, 0, y), Quaternion.identity);
                        newCasilla2.GetComponent<MeshRenderer>().material.color = Color.green;
                        break;
                    case Types.PowerUp:
                        GameObject newCasilla3 = Instantiate(cube, new Vector3(x, 0, y), Quaternion.identity);
                        newCasilla3.GetComponent<MeshRenderer>().material.color = Color.blue;
                        break;
                    case Types.Spike:
                        GameObject newCasilla4= Instantiate(cube, new Vector3(x, 0, y), Quaternion.identity);
                        newCasilla4.GetComponent<MeshRenderer>().material.color = Color.red;
                        break;
                    default:
                        break;
                }
            }
        }
    }
    public void PrintMap()
    {
        string linea = "";
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 20; j++)
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
                        matriz[z, i].owner = (Owner)playerColor;
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
                if (matriz[ar[1]+1, y].owner == (Owner)playerColor && !init)
                {
                    type = 0;
                }
                else if (matriz[ar[1] + 1, y].owner != (Owner)playerColor && !init)
                {
                    type = 2;
                }
                switch (type)
                {
                    case 0:
                        for (int i = ar[1] + 1; i < 10; i++)
                        {
                            if (matriz[i, y].owner != (Owner)playerColor || i >= 9)
                            {
                                temp= i-1;
                                break;
                            }
                        }
                        break;
                    case 2:
                        for (int i = ar[1] + 1; i < 10; i++)
                        {
                            if (matriz[i, y].owner == (Owner)playerColor)
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
                if (!init && matriz[i, y].owner == (Owner)player)
                {
                    ints[0] = Initial(i,y,player);
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
        if (matriz[start+1, y].owner == (Owner)player && !init)
        {
            type = 0;
        }
        else if (matriz[start+1, y].owner != (Owner)player && !init)
        {
            type = 2;
        }
        switch (type)
        {
            case 0:
                for (int i = start+1; i < 10; i++)
                {
                    if (matriz[i,y].owner!=(Owner)player || i>=9)
                    {
                        return i-1;
                    }
                }
                break;
            case 2:
                for (int i = start+1; i < 10; i++)
                {
                    if (matriz[i, y].owner == (Owner)player)
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
    public int Initial(int supposedStart,int y,int player)
    {
        bool salir = false;
        int ammount = 0;
        int valor = NextX(supposedStart, y, player);
        int i = NextX(supposedStart, y, player);
        while (!salir)
        {
            
            if (valor != -1)
            {
                valor = NextX(valor, y, player);
                ammount++;
            }
            else
            {
                salir = true;
            }
        }
        if (i!=-1 && ammount%2==0)
        {
            return i;
        }else
        {
            return supposedStart;
        }
    }
}
