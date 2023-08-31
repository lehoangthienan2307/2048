using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI floatText;
    [SerializeField] private CanvasGroup canvasGroup;
    private float moveTime = 0.6f;
    private float moveAmount = 60f;
    private float fadeTime = 0.6f;
    private float fadeDelay = 0.1f;
    public void Init(int number)
    {
        floatText.text = number.ToString();

        LeanTween.alphaCanvas(canvasGroup, 0, fadeTime).setDelay(fadeDelay);

        floatText.transform.LeanMoveLocalY(moveAmount, moveTime).setOnComplete( () => {
            Destroy(gameObject);
        });
    }
}
