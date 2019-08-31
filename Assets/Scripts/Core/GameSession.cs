using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] Text scoreText;
    [SerializeField] Canvas gameOverCanvas;

    void Start()
    {
        gameOverCanvas.enabled = false;
        scoreText.text = score.ToString();
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    public IEnumerator LoadGameOverScreen()
    {
        yield return new WaitForSeconds(2f); // wait before enable game over screen
        gameOverCanvas.enabled = true;
    }

}
