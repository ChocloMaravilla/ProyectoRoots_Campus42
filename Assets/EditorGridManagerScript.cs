using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditorGridManagerScript : MonoBehaviour
{
	public Sprite[] tileSprites;
	public Sprite[] tileValueSprites;
	Vector2Int gridSize = new Vector2Int(20, 20);
	int[][] rawGrid;
	int[][] rawValueGrid;
	public GameObject gridTile;
	Transform cursor;
	int brushTile;
	const int maxBrush = 4;
	bool onValueGrid;
	int brushValueTile;
	const int maxValueBrush = 7;
	string[] tileNames = new string[]
	{
		"Eraser >:)",
		"Spawn Tile :D",
		"Basic Tile :P",
		"Power-Up Tile :3",
		"Spiky Tile :O"
	};
	Transform grid;
	Transform cursorSelection;
	Transform valueGrid;
	Transform valueCursorSelection;
    // Start is called before the first frame update
    void Start()
    {
    	Camera.main.transform.position = new Vector3((gridSize.x / 2f - 0.5f) * 1.5f, gridSize.y / 2f - 0.5f, -10);
    	Camera.main.orthographicSize = Mathf.Max(gridSize.x, gridSize.y) / 2f;
        // Generate Grid
        rawGrid = new int[gridSize.y][];
        rawValueGrid = new int[gridSize.y][];
        grid = transform.GetChild(1);
        valueGrid = transform.GetChild(3);
        for (int y = 0; y < gridSize.y; y++)
        {
        	rawGrid[y] = new int[gridSize.x];
        	rawValueGrid[y] = new int[gridSize.x];
        	for (int x = 0; x < gridSize.x; x++)
	        {
	        	rawValueGrid[y][x] = 1;
	        	Instantiate(gridTile, new Vector2(x, gridSize.y - y - 1), Quaternion.identity, grid);
	        	SpriteRenderer r = Instantiate(gridTile, new Vector2(x, gridSize.y - y - 1), Quaternion.identity, valueGrid).GetComponent<SpriteRenderer>();
	        	r.sprite = tileValueSprites[1];
	        	r.enabled = false;
	        }
        }
        cursor = transform.GetChild(0);
        cursorSelection = transform.GetChild(2);
        cursorSelection.position = new Vector2((gridSize.x / 2f - 0.5f) * 3f, (gridSize.y / 2f - 0.5f) * 1.9f);
        valueCursorSelection = transform.GetChild(4);
        valueCursorSelection.position = new Vector2((gridSize.x / 2f - 0.5f) * 3f, (gridSize.y / 2f - 0.5f) * 1.9f - 1.5f);
        UpdateCursorSelection();
        valueGrid.gameObject.SetActive(false);
        valueCursorSelection.gameObject.SetActive(false);
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
    	// Toggle Grids
    	if (Input.GetKeyDown(KeyCode.V))
    	{
    		onValueGrid = !onValueGrid;
    		grid.gameObject.SetActive(!onValueGrid);
        	cursorSelection.gameObject.SetActive(!onValueGrid);
    		valueGrid.gameObject.SetActive(onValueGrid);
        	valueCursorSelection.gameObject.SetActive(onValueGrid);
        	UpdateCursorSelection();
    	}
    	// Paint Tile
		if ((Input.GetMouseButton(0) || Input.GetMouseButtonDown(1)) && (rawGrid[cursorGridPos.y][cursorGridPos.x] != 0 || !onValueGrid))
    	{
    		if (onValueGrid)
    		{
    			rawValueGrid[cursorGridPos.y][cursorGridPos.x] = brushValueTile;
    			valueGrid.GetChild((gridSize.y - 1 - cursorGridPos.y) * gridSize.x + cursorGridPos.x).GetComponent<SpriteRenderer>().sprite = GetTileValueSprite(brushValueTile);
    		}
    		else
    		{
    			rawGrid[cursorGridPos.y][cursorGridPos.x] = brushTile;
    			grid.GetChild((gridSize.y - 1 - cursorGridPos.y) * gridSize.x + cursorGridPos.x).GetComponent<SpriteRenderer>().sprite = GetTileSprite(brushTile);
    			valueGrid.GetChild((gridSize.y - 1 - cursorGridPos.y) * gridSize.x + cursorGridPos.x).GetComponent<SpriteRenderer>().enabled = brushTile != 0;
    		}
    	}
    	// Change Brush Tile
    	if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
    	{
    		if (onValueGrid)
    		{
    			if (Input.GetKeyDown(KeyCode.Z))
	    		{
	    			brushValueTile--;
	    			if (brushValueTile < 0) { brushValueTile = maxBrush; }
	    		}
	    		else
	    		{
	    			brushValueTile++;
	    			if (brushValueTile > maxValueBrush) { brushValueTile = 0; }
	    		}
    		}
    		else
    		{
    			if (Input.GetKeyDown(KeyCode.Z))
	    		{
	    			brushTile--;
	    			if (brushTile < 0) { brushTile = maxBrush; }
	    		}
	    		else
	    		{
	    			brushTile++;
	    			if (brushTile > maxBrush) { brushTile = 0; }
	    		}
    		}
    		UpdateCursorSelection();
    	}
    	// Toggle Cursor Visibility
    	if (Input.GetKeyDown(KeyCode.C)) { cursor.GetComponent<SpriteRenderer>().enabled = !cursor.GetComponent<SpriteRenderer>().enabled; }
    	// JSON
    	if (Input.GetKeyDown(KeyCode.Space))
    	{
    		string customBoardsPath = Application.streamingAssetsPath + "/CustomBoards/";
    		if (!Directory.Exists(customBoardsPath)) { Directory.CreateDirectory(customBoardsPath); }
    		int fileName = 0;
    		while (File.Exists(customBoardsPath + $"{fileName}.json")) { fileName++; }
    		using (StreamWriter sw = new StreamWriter(customBoardsPath + $"{fileName}.json")) { sw.Write(JsonUtility.ToJson(new BoardData(gridSize, rawGrid, rawValueGrid))); }
    		Quit();
    	}
    }
    void Quit() {}
    Sprite GetTileSprite(int _tileIndex) { return tileSprites[_tileIndex + 1]; }
    Sprite GetTileValueSprite(int _valueIndex) { return tileValueSprites[_valueIndex]; }
	void UpdateCursorSelection()
	{
		if (onValueGrid)
		{
			cursor.GetComponent<SpriteRenderer>().sprite = GetTileValueSprite(brushValueTile);
			for (int i = 0; i <= maxValueBrush; i++) { valueCursorSelection.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i == brushValueTile ? 1 : 0.25f); }
		}
		else
		{
			cursor.GetComponent<SpriteRenderer>().sprite = GetTileSprite(brushTile == 0 ? -1 : brushTile);
			cursorSelection.GetChild(0).GetComponent<TextMeshPro>().text = tileNames[brushTile];
			for (int i = 0; i <= maxBrush; i++) { cursorSelection.GetChild(i + 1).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, i == brushTile ? 1 : 0.25f); }
		}
	}
}