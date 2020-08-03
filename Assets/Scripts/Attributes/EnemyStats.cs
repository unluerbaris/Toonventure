using UnityEngine;

namespace Toon.Attributes
{
    public class EnemyStats : MonoBehaviour
    {
        [SerializeField] int enemyPoints = 50;

        public int EnemyPoints()
        {
            return enemyPoints;
        }
    }
}
