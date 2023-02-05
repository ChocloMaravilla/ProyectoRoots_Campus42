using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCode : MonoBehaviour
{

    public GameObject nivel, options, menu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Levels()
    {
        menu.SetActive(false);
        options.SetActive(false);
        nivel.SetActive(true);

    }

    public void Options()
    {
        options.SetActive(true);
        nivel.SetActive(false);
        menu.SetActive(false);
    }
    public void Menu()
    {
        options.SetActive(false);
        nivel.SetActive(false);
        menu.SetActive(true);
    }
    
    public void Exit()
    {
        Application.Quit();
    }


    public void ChangeScene(string path)
    {
        BoardData board= new BoardData();
        board.LoadFromFile("StreamingAssets/" + path);
        try
        {
            int[,] hola = board.GetRawTileMatrix();
            print(hola[0,0]);
            Menu();
        }
        catch
        {

        }
        //SceneManager.LoadScene(1);
    }
}

public static class Path
{
    public static TextAsset json;
}