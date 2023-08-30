using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public TileState tileState { get; private set; }
    public TileCell tileCell{ get; private set; }
    private Image backgroundImage;
    private TextMeshProUGUI textNumber;
    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
        textNumber = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void SetState(TileState state)
    {
        this.tileState = state;

        backgroundImage.color = state.backgroundColor;
        textNumber.color = state.textColor;
        textNumber.text = state.number.ToString();
    }
    public void Spawn(TileCell cell)
    {
        if (tileCell != null)
        {
            tileCell.tile = null;
        }
        
        tileCell = cell;
        tileCell.tile = this;
        transform.position = cell.transform.position;
    }
}
