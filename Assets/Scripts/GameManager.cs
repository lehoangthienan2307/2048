using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TileBoard tileBoard;
    private void Start()
    {
        NewGame();
    }
    public void NewGame()
    {
        tileBoard.ClearBoard();

        tileBoard.CreateTile();
        tileBoard.CreateTile();

        tileBoard.enabled = true;
    }
    public void GameOver()
    {
        tileBoard.enabled = false;
    }
}
