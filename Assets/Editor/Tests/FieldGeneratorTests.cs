using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using FifteenPuzzle;
using TMPro;

public class FieldGeneratorTests
{
    //[Test]
    //public void FieldGeneratorTestsSimplePasses()
    //{
    //    FieldGenerator fieldGenerator = new GameObject("Field Generator").AddComponent<FieldGenerator>();
    //    Tile tile = new GameObject("Tile").AddComponent<Tile>();
    //    List<Transform> startPositions = CreateStartPositions();

    //    fieldGenerator.StartPositions = startPositions;
    //    fieldGenerator.Tile = tile;
    //    fieldGenerator.TileCount = startPositions.Count;

    //    fieldGenerator.GenerateField();

    //    Assert.AreEqual(5, fieldGenerator.Tiles.Count);

    //    Assert.AreEqual(startPositions[0].position, fieldGenerator.Tiles[0].transform.position);
    //    Assert.AreEqual(startPositions[3].position, fieldGenerator.Tiles[3].transform.position);
    //    Assert.AreEqual(startPositions[4].position, fieldGenerator.Tiles[4].transform.position);
    //}

    //[Test]
    //public void CustomizeTilesTest()
    //{
    //    FieldGenerator fieldGenerator = new GameObject().AddComponent<FieldGenerator>();
    //    Tile tile = new GameObject("Tile").AddComponent<Tile>();
    //    List<Transform> startPositions = CreateStartPositions();

    //    tile.CreateNumberText();

    //    fieldGenerator.StartPositions = startPositions;
    //    fieldGenerator.Tile = tile;
    //    fieldGenerator.TileCount = startPositions.Count;

    //    fieldGenerator.GenerateField();

    //    fieldGenerator.CustomizeTiles();

    //    Assert.AreEqual("1", fieldGenerator.Tiles[0].GetNumberText());
    //    Assert.AreEqual("4", fieldGenerator.Tiles[3].GetNumberText());
    //}

    //private List<Transform> CreateStartPositions()
    //{
    //    List<Transform> startPositions = new List<Transform>
    //    {
    //        new GameObject("Start Position").transform,
    //        new GameObject("Start Position").transform,
    //        new GameObject("Start Position").transform,
    //        new GameObject("Start Position").transform,
    //        new GameObject("Start Position").transform
    //    };

    //    startPositions[0].transform.position = new Vector3(-1.5f, 1.5f, 0);
    //    startPositions[1].transform.position = new Vector3(-0.5f, 1.5f, 0);
    //    startPositions[2].transform.position = new Vector3(0.5f, 1.5f, 0);
    //    startPositions[3].transform.position = new Vector3(1.5f, 1.5f, 0);
    //    startPositions[4].transform.position = new Vector3(1.5f, 0.5f, 0);

    //    return startPositions;
    //}
}
