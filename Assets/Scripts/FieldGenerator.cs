using System.Collections.Generic;
using UnityEngine;

namespace FifteenPuzzle
{
    public class FieldGenerator : MonoBehaviour
    {
        public int TileCount = 15;

        public Transform Parent = null;
        public Tile Tile = null;
        public List<Transform> StartPositions = new();
        public List<Tile> Tiles = new();

        private List<Vector2> directions = new()
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right
        };
        private List<Vector3> origins = new()
        {
            new Vector3(0, 0.5125f),
            new Vector3(0, -0.5125f),
            new Vector3(-0.5125f, 0),
            new Vector3(0.5125f, 0),
        };

        public System.Action FieldCreated;

        private void Start()
        {
            GenerateField();
            CustomizeTiles();
            FieldCreated?.Invoke();
        }

        public void GenerateField()
        {
            for (int i = 0; i < TileCount; i++)
            {
                var tile = Instantiate(Tile);
                tile.transform.position = StartPositions[i].position;
                tile.transform.parent = Parent;
                Tiles.Add(tile);
            }
        }

        public void CustomizeTiles()
        {
            for (int i = 0; i < Tiles.Count; i++)
            {
                Tiles[i].SetNumber(i + 1);
            }
        }

        public void ShuffleTiles()
        {
            List<Tile> tiles = new();

            for (int i = 0; i < Tiles.Count; i++)
            {
                tiles.Add(Tiles[i]);
            }

            tiles.Shuffle();

            for (int i = 0; i < tiles.Count; i++)
            {
                tiles[i].transform.position = StartPositions[i].transform.position;
            }
        }
    }
}