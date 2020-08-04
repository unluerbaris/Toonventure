using UnityEngine;

namespace Toon.Attributes
{
    public class EnemyStats : MonoBehaviour
    {
        [SerializeField] int enemyPoints = 80;

        public int GetPoints()
        {
            return enemyPoints;
        }
    }
}
