using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FifteenPuzzle
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private string _key;

        [SerializeField] private int _numberOfMoves = 0;

        [SerializeField] private FieldGenerator _fieldGenerator = null;
        [SerializeField] private List<Transform> _transforms = new();

        [SerializeField] private GameObject _background = null;
        [SerializeField] private GameObject _tilesParent = null;

        [SerializeField] private float _timeToWait = 0.5f;

        [SerializeField] private int _tilesCount;
        private List<Tile> _tiles = new();
        private AudioManager _audioManager = null;
        private Results _results = null;
        private UI _ui = null;

        public System.Action GameWon;
        public System.Action GameRestarted;
        public event System.EventHandler<int> OnNumberOfMovesChanged;

        [DllImport("__Internal")]
        private static extern void ShowInterstitialAd();

        [DllImport("__Internal")]
        private static extern void SaveData(string key, string value);


        private void Start()
        {
            _ui = FindObjectOfType<UI>();
            _audioManager = FindObjectOfType<AudioManager>();
            _results = FindObjectOfType<Results>();

            _ui.RestartGameClickedAction += RestartGame;
            _ui.ShuffleButtonClickedAction += ShuffleField;

            _fieldGenerator.TilesCreated += TilesCreated;

            ShowInterstitialAd();
        }

        private void ShuffleField()
        {
            _numberOfMoves = 0;
            OnNumberOfMovesChanged?.Invoke(this, _numberOfMoves);
            _fieldGenerator.RegenerateNumbers(true);
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

            OnNumberOfMovesChanged.Invoke(this, _numberOfMoves);

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

            int numbersOfMoves = _results.GetResult(SceneManager.GetActiveScene().name);
            
            if (numbersOfMoves == 0 || numbersOfMoves > _numberOfMoves)
            {
                SaveData(SceneManager.GetActiveScene().name, _numberOfMoves.ToString());
            }

            _tilesParent.gameObject.SetActive(false);
            _background.gameObject.SetActive(false);
            GameWon?.Invoke();
        }

        public int GetNumberOfMoves()
        {
            return _numberOfMoves;
        }
    }
}