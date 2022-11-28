using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FifteenPuzzle
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private FieldGenerator _fieldGenerator = null;
        [SerializeField] private List<Transform> _transforms = new();

        [SerializeField] private GameObject _background = null;
        [SerializeField] private GameObject _tilesParent = null;

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
            _ui.ShuffleButtonClickedAction += Shuffle;

            _fieldGenerator.FieldCreated += FieldCreated;
        }

        private void RestartGame()
        {
            _background.gameObject.SetActive(true);
            _tilesParent.gameObject.SetActive(true);
            GameRestarted?.Invoke();
        }

        private void Shuffle()
        {
            _fieldGenerator.ShuffleTiles();
        }

        private void FieldCreated()
        {
            _tiles = FindObjectsOfType<Tile>().ToList();

            foreach (var tile in _tiles)
            {
                tile.Clicked += Clicked;
            }
        }

        private void Clicked()
        {
            _audioManager.PlayClickingClip();

            List<Tile> tiles = new();

            int correctAnswers = 0;

            foreach (var item in _transforms)
            {
                RaycastHit2D hit = Physics2D.Raycast(item.position, Vector2.zero);

                if (hit.transform != null)
                {
                    tiles.Add(hit.transform.GetComponent<Tile>());
                }
            }


            for (int i = 0; i < tiles.Count; i++)
            {
                if (tiles[i].GetNumberText() == $"{i + 1}")
                {
                    correctAnswers++;
                }
            }

            if (correctAnswers == 15)
            {
                _tilesParent.gameObject.SetActive(false);
                _background.gameObject.SetActive(false);
                GameWon?.Invoke();
            }
        }
    }
}
