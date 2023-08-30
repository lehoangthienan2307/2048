using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBoard : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private TileState[] tileStates;
    [SerializeField] private TileGrid tileGrid;
    private List<Tile> tiles;
    private bool waiting;
    private void Awake()
    {
        tiles = new List<Tile>();
    }
    public void ClearBoard()
    {
        foreach (TileCell cell in tileGrid.cells)
        {
            cell.tile = null;
        }
        
        foreach (Tile tile in tiles)
        {
            Destroy(tile.gameObject);
        }
        tiles.Clear();
    }
    public void CreateTile()
    {
        Tile tile = Instantiate(tilePrefab, tileGrid.transform);
        tile.SetState(tileStates[0]);
        tile.Spawn(tileGrid.GetRandomEmptyCell());
        tiles.Add(tile);
    }
    private void Update()
    {
        if (!waiting)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveTiles(Vector2Int.up, 0, 1, 1, 1);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveTiles(Vector2Int.down, 0, 1, tileGrid.height - 2, -1);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveTiles(Vector2Int.left, 1, 1, 0, 1);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveTiles(Vector2Int.right, tileGrid.width - 2, -1, 0, 1);
            }
        }
        
    }
    private void MoveTiles(Vector2Int direction, int startX, int incrementX, int startY, int incrementY)
    {
        bool changed = false;
        for (int x = startX; x >= 0 && x < tileGrid.width; x += incrementX)
        {
            for (int y = startY; y>= 0 && y < tileGrid.height; y += incrementY)
            {
                TileCell tileCell = tileGrid.GetCell(x,y);

                if (tileCell.occupied)
                {
                    changed |= MoveTile(tileCell.tile, direction);
                }
            }
        }
        if (changed)
        {
            StartCoroutine(WaitForChanges());
        }
    }
    private bool MoveTile(Tile tile, Vector2Int direction)
    {
        TileCell newCell = null;
        TileCell adjacentCell = tileGrid.GetAdjacentCell(tile.tileCell, direction);
        
        while (adjacentCell != null)
        {
            if (adjacentCell.occupied)
            {
                if (CanMerge(tile, adjacentCell.tile))
                {
                    Merge(tile, adjacentCell.tile);
                    return true;
                }
                break;
            }
            newCell = adjacentCell;
            adjacentCell = tileGrid.GetAdjacentCell(adjacentCell, direction);
        }

        if (newCell != null)
        {
            tile.MoveTo(newCell);
            return true;
        }
        return false;
    }
    private bool CanMerge(Tile a, Tile b)
    {
        return a.tileState.number == b.tileState.number && !b.locked;    
    }
    private void Merge(Tile a, Tile b)
    {
        tiles.Remove(a);
        a.Merge(b.tileCell);    

        int index = Math.Clamp(IndexOf(b.tileState) + 1, 0, tileStates.Length-1);
        b.SetState(tileStates[index]);
    }
    private int IndexOf(TileState tileState)
    {
        for (int i=0; i < tileStates.Length; i++)
        {
            if (tileStates[i] == tileState)
            {
                return i;
            }
        }
        return -1;
    }
    private IEnumerator WaitForChanges()
    {
        waiting = true;
        yield return new WaitForSeconds(0.1f);
        waiting = false;
        
        foreach (Tile tile in tiles)
        {
            tile.locked = false;
        }
        if (tiles.Count != tileGrid.size)
        {
            CreateTile();
        }
        if (CheckForGameOver())
        {
            gameManager.GameOver();
        }
    }
    private bool CheckForGameOver()
    {
        if (tiles.Count != tileGrid.size)
        {
            return false;
        }

        foreach (Tile tile in tiles)
        {
            TileCell up = tileGrid.GetAdjacentCell(tile.tileCell, Vector2Int.up);
            TileCell down = tileGrid.GetAdjacentCell(tile.tileCell, Vector2Int.down);
            TileCell left = tileGrid.GetAdjacentCell(tile.tileCell, Vector2Int.left);
            TileCell right = tileGrid.GetAdjacentCell(tile.tileCell, Vector2Int.right);

            if (up != null && CanMerge(tile, up.tile))
            {
                return false;
            }
            if (down != null && CanMerge(tile, down.tile))
            {
                return false;
            }
            if (left != null && CanMerge(tile, left.tile))
            {
                return false;
            }
            if (right != null && CanMerge(tile, right.tile))
            {
                return false;
            }
            
        }
        return true;
    }
}
