using UnityEngine;
using UnityEngine.SceneManagement;

public class aaaaaaaa : MonoBehaviour
{
    public GameCode gameCode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            SceneManager.LoadScene("Editor");
        }
        if (Input.GetKeyUp(KeyCode.V) && System.IO.File.Exists(Application.streamingAssetsPath + $"/a.json"))
        {
            gameCode.ChangeScene("a.json");
        }
    }
}