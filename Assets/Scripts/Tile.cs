using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public TileState tileState { get; private set; }
    public TileCell tileCell{ get; private set; }
    public bool locked { get; set; }
    private Image backgroundImage;
    private TextMeshProUGUI textNumber;
    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
        textNumber = GetComponentInChildren<TextMeshProUGUI>();
    }
    
    public void SetState(TileState state)
    {
        tileState = state;

        backgroundImage.color = state.backgroundColor;
        textNumber.color = state.textColor;
        textNumber.text = state.number.ToString();
    }
    public void Spawn(TileCell cell)
    {
        transform.localScale = Vector2.zero;

        if (tileCell != null)
        {
            tileCell.tile = null;
        }
        
        tileCell = cell;
        tileCell.tile = this;
        transform.position = cell.transform.position;

        transform.LeanScale(Vector2.one, 0.2f);
        
    }
    public void Merge(TileCell cell, float moveTime)
    {
        if (tileCell != null)
        {
            tileCell.tile = null;
        }
        tileCell = null;
        cell.tile.locked = true;

        transform.LeanMove(cell.transform.position, moveTime).setOnComplete( () => { 
            Destroy(gameObject);
            cell.tile.transform.localScale = Vector2.zero;
            float scaleTime = 0.5f;
            cell.tile.transform.LeanScale(Vector2.one, scaleTime).setEaseOutElastic();
        });
    }
    public void MoveTo(TileCell cell, float moveTime)
    {
        if (tileCell != null)
        {
            tileCell.tile = null;
        }
        
        tileCell = cell;
        tileCell.tile = this;
        transform.LeanMove(cell.transform.position, moveTime);
    }
    
}
