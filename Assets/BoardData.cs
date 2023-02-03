using UnityEngine;

public class BoardData
{
	public Vector2Int size;
	public int[] rawTiles;
	public BoardData(Vector2Int _size, int[][] _rawTiles)
	{
		size = _size;
		rawTiles = new int[size.x * size.y];
		for (int y = 0; y < size.y; y++)
		{
			for (int x = 0; x < size.x; x++)
			{
				rawTiles[y * size.x + x] = _rawTiles[y][x];
			}
		}
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
}