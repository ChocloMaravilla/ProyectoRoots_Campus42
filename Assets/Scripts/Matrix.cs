using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Matrix : MonoBehaviour
{
    [SerializeField] private GameObject spawnCasilla, basicsCasilla, cesped, pupCasilla, spikeCasilla;

    public GameObject cube;
    public GameObject playerObj;
    public GameObject enemyObj;
    Entities[] entities;
    public int[,] matrix;
    public Casilla[,] matriz = new Casilla[20, 20];
    public BoardData data = new BoardData();
    // Start is called before the first frame update
    void Start()
    {
        data.LoadFromFile(Path.json);
        matrix = data.GetRawTileMatrix();
        int[,] valuesMatrix = data.GetRawTileValueMatrix();
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 20; y++)
            {
                matriz[x, y] = new Casilla();
                matriz[x, y].type = (Types)matrix[x, y];
                matriz[x, y].value = (Values)valuesMatrix[x, y];
            }
        }
        InstantiateDebugMap();
        InstantiateCesped();
        SetSpawns();
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
                switch (matriz[x, y].type)
                {
                    case Types.None:
                        break;
                    case Types.Spawn:
                        //GameObject newCasilla = Instantiate(spawnCasilla, new Vector3(x, 0, y), Quaternion.identity);
                        //newCasilla.GetComponent<MeshRenderer>().material.color = Color.yellow;
                        break;
                    case Types.Basics:
                        GameObject newCasilla2 = Instantiate(basicsCasilla, new Vector3(x, 0, y), Quaternion.identity);
                        break;
                    case Types.PowerUp:
                        GameObject newCasilla3 = Instantiate(pupCasilla, new Vector3(x, 0, y), Quaternion.identity);
                        break;
                    case Types.Spike:
                        GameObject newCasilla4 = Instantiate(spikeCasilla, new Vector3(x, 0, y), Quaternion.identity);
                        break;
                    default:
                        break;
                }
            }
        }
    }
    public void InstantiateCesped()
    {
        int cantidad = Random.Range(50, 150);
        for (int i = 0; i < cantidad; i++)
        {
            bool destroyer = false;
            GameObject newCasilla2 = Instantiate(cesped, new Vector3(Random.Range(0, 20f), 0, Random.Range(0, 20f)), Quaternion.identity);
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    if (matriz[x, y].type == Types.None && Vector3.Distance(newCasilla2.transform.position, new Vector3(x, 0, y)) < 1f)
                    {
                        Destroy(newCasilla2);
                        destroyer = true;
                        break;
                    }
                }
                if (destroyer == true)
                {
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
                linea = linea + matrix[i, j];
            }
            linea = linea + "\n";
        }
        print(linea);
    }
    public void SetTileOwner(Vector2Int coords, Owner owner)
    {
        bool ownershipConflict = false;
        matriz[coords.x, coords.y].owner = ownershipConflict ? Owner.None : owner;
    }
    public bool CanStepOnTile(Vector2Int targetCoords)
    {
        if (targetCoords.x < 0 || targetCoords.y < 0 || targetCoords.x > 19 || targetCoords.y > 19) { return false; }
        return matriz[targetCoords.x, targetCoords.y].CanBeSteppedOn();
    }
    void SetSpawns()
    {
        List<Vector2Int> spawnPos = new List<Vector2Int>();
        for (int y = 0; y < 20; y++)
        {
            for (int x = 0; x < 20; x++)
            {
                if (matriz[x, y].type == Types.Spawn) { spawnPos.Add(new Vector2Int(x, y)); }
            }
        }
        Vector2Int[] usedSpawns = new Vector2Int[4];
        for (int y = 0; y < 4; y++)
        {
            int n = Random.Range(0, spawnPos.Count);
            usedSpawns[y] = spawnPos[n];
            spawnPos.RemoveAt(n);
        }
        entities = new Entities[4];
        for (int y = 0; y < 4; y++)
        {
            entities[y] = Instantiate(y == 0 ? playerObj : enemyObj).GetComponent<Entities>();
            entities[y].matriz = this;
            entities[y].spawnPos = usedSpawns[y];
            Owner[] ownerships = new Owner[4]
            {
                Owner.Blue,
                Owner.Red,
                Owner.Grean,
                Owner.Yellow
            };
            entities[y].ownershipType = ownerships[y];
            Instantiate(spawnCasilla, new Vector3(usedSpawns[y].y, 0, usedSpawns[y].x), Quaternion.identity).transform.GetChild(0).GetComponent<MeshRenderer>().materials[1].color = PlayerColors.GetColor(ownerships[y]);
        }
    }
    public void OnEntityDefeated()
    {
        int aliveEntities = 0;
        foreach (Entities entitiy in entities)
        {
            if (entitiy.gameObject.activeInHierarchy) { aliveEntities++; }
        }
        if (aliveEntities < 2)
        {
            foreach (Entities entitiy in entities)
            {
                if (entitiy.gameObject.activeInHierarchy)
                {
                    switch (entitiy.ownershipType)
                    {
                        case Owner.Blue:
                            VirtualRAM.playerVictories++;
                            break;
                        case Owner.Red:
                            VirtualRAM.bot1Victories++;
                            break;
                        case Owner.Grean:
                            VirtualRAM.bot2Victories++;
                            break;
                        case Owner.Yellow:
                            VirtualRAM.bot3Victories++;
                            break;
                    }
                }
            }
            SceneManager.LoadScene("UIScene");
        }
    }
}