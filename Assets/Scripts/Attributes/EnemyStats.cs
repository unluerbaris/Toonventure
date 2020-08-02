using UnityEngine;

namespace Toon.Attributes
{
    public class EnemyStats : MonoBehaviour
    {
        [SerializeField] int enemyPoints = 50;
        [SerializeField] AudioClip enemyDeathSFX;
        [Range(0, 1)] [SerializeField] float enemyDeathVolume = 0.25f;
        GameObject audioListener;

        void Start()
        {
            audioListener = GameObject.FindWithTag("AudioListener");
        }

        public void PlayDeathSFX()
        {
            AudioSource.PlayClipAtPoint(enemyDeathSFX, audioListener.transform.position, enemyDeathVolume);
        }

        public int EnemyPoints()
        {
            return enemyPoints;
        }
    }
}
