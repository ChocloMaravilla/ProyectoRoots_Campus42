using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorGridManagerScript : MonoBehaviour
{
	Vector2Int gridSize = new Vector2Int(10, 10);
	int[][] rawGrid;
	public GameObject gridTile;
	Transform cursor;
	int brushTile;
	const int maxBrush = 2;
    // Start is called before the first frame update
    void Start()
    {
    	Camera.main.transform.position = new Vector3(gridSize.x / 2f - 0.5f, gridSize.y / 2f - 0.5f, -10);
        cursor = transform.GetChild(0);
        // Generate Grid
        rawGrid = new int[gridSize.y][];
        for (int y = 0; y < gridSize.y; y++)
        {
        	rawGrid[y] = new int[gridSize.x];
        	for (int x = 0; x < gridSize.x; x++)
	        {
	        	Instantiate(gridTile, new Vector2(x, gridSize.y - y - 1), Quaternion.identity, transform.GetChild(1));
	        }
        }
    }

    // Update is called once per frame
    void Update()
    {
    	// Snap Cursor To Grid
    	Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10);
        Vector2Int cursorGridPos = new Vector2Int(0, 0);
        cursorGridPos.x = Mathf.Clamp(Mathf.RoundToInt(cursorPos.x), 0, gridSize.x - 1);
        cursorGridPos.y = Mathf.Clamp(Mathf.RoundToInt(cursorPos.y), 0, gridSize.y - 1);
    	cursor.position = (Vector2)cursorGridPos;
    	// Paint Tile
    	if (Input.GetMouseButtonDown(0))
    	{
    		rawGrid[cursorGridPos.y][cursorGridPos.x] = brushTile;
    		transform.GetChild(1).GetChild((gridSize.y - 1 - cursorGridPos.y) * gridSize.x + cursorGridPos.x).GetComponent<SpriteRenderer>().color = GetTileColor(brushTile);
    	}
    	// Toggle Cursor Visibility
    	if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
    	{
    		if (Input.GetKeyDown(KeyCode.Z))
    		{
    			brushTile--;
    			if (brushTile == 0) { brushTile = maxBrush; }
    		}
    		else
    		{
    			brushTile++;
    			if (brushTile > maxBrush) { brushTile = 0; }
    		}
    		cursor.GetComponent<SpriteRenderer>().color = GetTileColor(brushTile == 0 ? -1 : brushTile);
    	}
    	// Toggle Cursor Visibility
    	if (Input.GetKeyDown(KeyCode.C)) { cursor.GetComponent<SpriteRenderer>().enabled = !cursor.GetComponent<SpriteRenderer>().enabled; }
    	// JSON
    	if (Input.GetKeyDown(KeyCode.Space)) { print(JsonUtility.ToJson(new BoardData(gridSize, rawGrid))); }
    }
    Color GetTileColor(int _tileIndex)
    {
    	switch (_tileIndex)
    	{
    		// Cursor Default Color
    		case -1: return Color.white;
    		// Spawn Tile
    		case 1: return Color.yellow;
    		// Spawn Tile
    		case 2: return Color.HSVToRGB(285f / 360f, 1, 1);
    		// Empty Tile
    		default: return new Color(0.25f, 0.25f, 0.25f, 0.5f);
    	}
    }
}