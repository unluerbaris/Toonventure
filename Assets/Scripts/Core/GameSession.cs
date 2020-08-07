using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Toon.Core
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] int score;
        [SerializeField] Text scoreText;
        [SerializeField] Canvas gameOverCanvas;
        [SerializeField] Canvas winCanvas;

        void Awake()
        {
            Time.timeScale = 1;
            gameOverCanvas.gameObject.SetActive(false);
            winCanvas.gameObject.SetActive(false);
            scoreText.text = score.ToString();
        }

        public void AddScore(int pointsToAdd)
        {
            score += pointsToAdd;
            scoreText.text = score.ToString();
        }

        public IEnumerator LoadGameOverScreen()
        {
            yield return new WaitForSeconds(2f); // wait before enable game over screen
            gameOverCanvas.gameObject.SetActive(true);
        }

        public IEnumerator LoadWinScreen()
        {
            yield return new WaitForSeconds(1f);
            winCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
