using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FifteenPuzzle
{
	public class FieldGenerator : MonoBehaviour
	{
		[SerializeField] private Tile _tilePrefab;

		[SerializeField] private Transform _tilesParent;
		[SerializeField] private Transform _tilesPositionsParent;
		[SerializeField] private List<Transform> _tilesPositions;

		[SerializeField] private int _tileCount;

		[SerializeField] private List<Tile> _tiles;

		public event EventHandler<List<Tile>> TilesCreated;

		private void Start()
		{
			GetPositions();
			GenerateTiles();
			RegenerateNumbers();

			TilesCreated?.Invoke(this, _tiles);
		}

		public void RegenerateNumbers()
		{
			GenerateNumbersOnTiles();

			bool canSolved = CheckIfPuzzleCanBeSolved();

			while (!canSolved)
			{
				GenerateNumbersOnTiles();
				canSolved = CheckIfPuzzleCanBeSolved();
			}
		}

		private void GenerateNumbersOnTiles()
		{
			List<int> numbers = new List<int>();

			for (int i = 1; i <= _tileCount; i++)
			{
				numbers.Add(i);
			}

			numbers.Shuffle();

			for (int i = 0; i < _tiles.Count; i++)
			{
				_tiles[i].SetNumber(numbers[i]);
			}
		}

		private void GenerateTiles()
		{
			for (int i = 0; i < _tileCount; i++)
			{
				Tile tile = Instantiate(_tilePrefab);

				tile.transform.position = _tilesPositions[i].position;
				tile.transform.parent = _tilesParent;

				_tiles.Add(tile);
			}
		}

		private void GetPositions()
		{
			foreach (Transform position in _tilesPositionsParent)
			{
				_tilesPositions.Add(position);
			}
		}

		private bool CheckIfPuzzleCanBeSolved()
		{
			int[] n = new int[_tileCount];

			for (int i = 0; i < _tileCount; i++)
			{
				int value = _tiles[i].GetNumber();
				int count = 0;

				for (int j = i + 1; j < _tileCount; j++)
				{
					if (_tiles[j].GetNumber() < value)
					{
						count++;
					}
				}

				n[value - 1] = count;
			}

			bool isSolvable = (n.Sum() % 2 == 0);


			return isSolvable;
		}
	}
}