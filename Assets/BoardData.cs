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
		for (int y = 0; y < size.y; y++)
		{
			for (int x = 0; x < size.x; x++)
			{
				rawTiles[y * size.x + x] = _rawTiles[y][x];
				rawValueTiles[y * size.x + x] = _rawValueTiles[y][x];
			}
		}
	}
	public void LoadFromFile(string _jsonPath)
	{
		string jsonData = "";
		using (StreamReader sr = new StreamReader(_jsonPath)) { jsonData = sr.ReadToEnd(); }
		JsonUtility.FromJsonOverwrite(jsonData, this);
	}
	public int[][] GetRawTileMatrix()
	{
		int[][] returnMatrix = new int[size.y][];
		for (int y = 0; y < size.y; y++)
		{
			returnMatrix[y] = new int[size.x];
			for (int x = 0; x < size.x; x++)
			{
				returnMatrix[y][x] = rawTiles[y * size.x + x];
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