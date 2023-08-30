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
    public void Merge(TileCell cell)
    {
        if (tileCell != null)
        {
            tileCell.tile = null;
        }
        tileCell = null;
        cell.tile.locked = true;
        StartCoroutine(Animate(cell.transform.position, false));
    }
    public void MoveTo(TileCell cell)
    {
        if (tileCell != null)
        {
            tileCell.tile = null;
        }
        
        tileCell = cell;
        tileCell.tile = this;
        StartCoroutine(Animate(cell.transform.position, true));
    }
    private IEnumerator Animate(Vector3 to, bool merging)
    {
        float elapsed = 0f;
        float duration= 0.1f;
        Vector3 from = transform.position;

        while (elapsed < duration) 
        {
            transform.position = Vector3.Lerp(from, to, elapsed/duration);
            elapsed += Time.deltaTime;
            yield return null;
        } 

        transform.position = to;

        if (merging)
        {
            Destroy(gameObject);
        }
    }
}
