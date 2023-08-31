using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private TileBoard tileBoard;
    [SerializeField] private CanvasGroup gameOver;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestText;
    private const string BEST_SCORE = "best";
    private int score;
    private void Start()
    {
        NewGame();
        soundManager.Play(SoundId.BGM_1);
    }
    public void NewGame()
    {
        SetScore(0);
        bestText.text = LoadBestScore().ToString();
        
        gameOver.alpha = 0f;
        gameOver.interactable = false;

        tileBoard.ClearBoard();

        tileBoard.CreateTile();
        tileBoard.CreateTile();

        tileBoard.enabled = true;
    }
    public void GameOver()
    {
        tileBoard.enabled = false;
        gameOver.interactable = true;
        StartCoroutine(Fade(gameOver, 1f, 1f));
    }
    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;
        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = to;
    }
    public void IncreaseScore(int points)
    {
        soundManager.Play(SoundId.SFX_1);
        SetScore(score + points);
    }
    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
        SaveBestScore();
    }
    private void SaveBestScore()
    {
        int best = LoadBestScore();
        if (score > best)
        {
            PlayerPrefs.SetInt(BEST_SCORE, score);
        }
    }
    private int LoadBestScore()
    {
        return PlayerPrefs.GetInt(BEST_SCORE, 0);
    }
}
