using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FifteenPuzzle
{
	public class Game : MonoBehaviour
	{
		[SerializeField] private int _numberOfMoves = 0;

		[SerializeField] private FieldGenerator _fieldGenerator = null;
		[SerializeField] private List<Transform> _transforms = new();

		[SerializeField] private GameObject _background = null;
		[SerializeField] private GameObject _tilesParent = null;

		[SerializeField] private float _timeToWait = 0.5f;

		[SerializeField] private int _tilesCount;
		private List<Tile> _tiles = new();
		private AudioManager _audioManager = null;
		private UI _ui = null;

		public System.Action GameWon;
		public System.Action GameRestarted;

		private void Start()
		{
			_ui = FindObjectOfType<UI>();
			_audioManager = FindObjectOfType<AudioManager>();

			_ui.RestartGameClickedAction += RestartGame;
			_ui.ShuffleButtonClickedAction += ShuffleField;

			_fieldGenerator.TilesCreated += TilesCreated;
		}

		private void ShuffleField()
		{
			_fieldGenerator.RegenerateNumbers();
		}

		private void TilesCreated(object sender, List<Tile> tiles)
		{
			foreach (Tile tile in tiles)
			{
				tile.Clicked += TileClicked;
				_tiles.Add(tile);
			}

			_tilesCount = tiles.Count;
		}

		private void RestartGame()
		{
			_background.gameObject.SetActive(true);
			_tilesParent.gameObject.SetActive(true);

			_numberOfMoves = 0;
			ShuffleField();

			GameRestarted?.Invoke();
		}

		private void TileClicked()
		{
			_numberOfMoves++;
			_audioManager.PlayClickingClip();

			if (CheckIsTileCollected()) StartCoroutine(GamePassed());
		}

		private bool CheckIsTileCollected()
		{
			return CheckCorrectTiles(GetTiles()) == _tilesCount;
		}

		private int CheckCorrectTiles(List<Tile> tiles)
		{
			int correctAnswers = 0;

			for (int i = 0; i < tiles.Count; i++)
			{
				if (tiles[i].GetNumber() == (i + 1))
				{
					correctAnswers++;
				}
			}

			return correctAnswers;
		}

		private List<Tile> GetTiles()
		{
			List<Tile> tiles = new();

			foreach (var item in _transforms)
			{
				RaycastHit2D hit = Physics2D.Raycast(item.position, Vector2.zero);

				if (hit.transform != null)
				{
					tiles.Add(hit.transform.GetComponent<Tile>());
				}
			}

			return tiles;
		}

		private IEnumerator GamePassed()
		{
			yield return new WaitForSeconds(_timeToWait);

			_tilesParent.gameObject.SetActive(false);
			_background.gameObject.SetActive(false);
			GameWon?.Invoke();
		}
	}
}