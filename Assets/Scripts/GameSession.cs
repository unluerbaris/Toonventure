using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] Text scoreText;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString();
        player = FindObjectOfType<Player>();
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

}
