using System.IO;
using UnityEngine;

public class BoardData
{
	public Vector2Int size;
	public int[] rawTiles;
	public int[] rawValueTiles;
	public BoardData() { }
	public BoardData(Vector2Int _size, int[][] _rawTiles, int[][] _rawValueTiles)
	{
		size = _size;
		rawTiles = new int[size.x * size.y];
		rawValueTiles = new int[size.x * size.y];
		for (int y = 0; y < _size.y; y++)
		{
			for (int x = 0; x < _size.x; x++)
			{
				rawTiles[y * size.x + x] = _rawTiles[y][x];
				rawValueTiles[y * size.x + x] = _rawValueTiles[y][x];
			}
		}
		return;
		// Determine X Bounds
		Vector2Int xBounds = new Vector2Int(0, 0);
		for (int x = 0; x < _size.x; x++)
		{
			for (int y = 0; y < _size.y; y++)
			{
				if (_rawTiles[y][x] != 0)
				{
					xBounds.y = x;
					break;
				}
			}
		}
		for (int x = _size.x - 1; x >= 0; x--)
		{
			for (int y = 0; y < _size.y; y++)
			{
				if (_rawTiles[y][x] != 0)
				{
					xBounds.x = x;
					break;
				}
			}
		}
		// Determine Y Bounds
		Vector2Int yBounds = new Vector2Int(0, 0);
		for (int y = 0; y < _size.y; y++)
		{
			for (int x = xBounds.x; x <= xBounds.y; x++)
			{
				if (_rawTiles[y][x] != 0)
				{
					yBounds.y = y;
					break;
				}
			}
		}
		for (int y = _size.y - 1; y >= 0; y--)
		{
			for (int x = xBounds.x; x <= xBounds.y; x++)
			{
				if (_rawTiles[y][x] != 0)
				{
					yBounds.x = y;
					break;
				}
			}
		}
		// Re-Adjust Size And Grid
		size = new Vector2Int(xBounds.y - xBounds.x + 1, yBounds.y - yBounds.x + 1);
		rawTiles = new int[size.x * size.y];
		rawValueTiles = new int[size.x * size.y];
		int y2 = 0;
		for (int y = yBounds.x; y <= yBounds.y; y++)
		{
			int x2 = 0;
			for (int x = xBounds.x; x <= xBounds.y; x++)
			{
				rawTiles[y2 * size.x + x2] = _rawTiles[y][x];
				rawValueTiles[y2 * size.x + x2] = _rawValueTiles[y][x];
				x2++;
			}
			y2++;
		}
	}
	public void LoadFromFile(string _jsonPath)
	{
		string jsonData = "";
		using (StreamReader sr = new StreamReader(_jsonPath)) { jsonData = sr.ReadToEnd(); }
		JsonUtility.FromJsonOverwrite(jsonData, this);
	}
	public int[,] GetRawTileMatrix()
	{
		int[,] returnMatrix = new int[size.y, size.x];
		for (int y = 0; y < size.y; y++)
		{
			//returnMatrix[y] = new int[size.x];
			for (int x = 0; x < size.x; x++)
			{
				returnMatrix[y,x] = rawTiles[y * size.x + x];
			}
		}
		return returnMatrix;
	}
	public int[][] GetRawTileValueMatrix()
	{
		int[][] returnMatrix = new int[size.y][];
		for (int y = 0; y < size.y; y++)
		{
			returnMatrix[y] = new int[size.x];
			for (int x = 0; x < size.x; x++)
			{
				returnMatrix[y][x] = rawValueTiles[y * size.x + x];
			}
		}
		return returnMatrix;
	}
}