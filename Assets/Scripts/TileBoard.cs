using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBoard : MonoBehaviour
{
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private TileState[] tileStates;
    [SerializeField] private TileGrid tileGrid;
    private List<Tile> tiles;
    private void Awake()
    {
        tiles = new List<Tile>();
    }
    private void Start()
    {
        CreateTile();
        CreateTile();
    }
    private void CreateTile()
    {
        Tile tile = Instantiate(tilePrefab, tileGrid.transform);
        tile.SetState(tileStates[0]);
        tile.Spawn(tileGrid.GetRandomEmptyCell());
        tiles.Add(tile);
    }
}
