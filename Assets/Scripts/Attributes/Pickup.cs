using UnityEngine;
using Toon.Core;

namespace Toon.Attributes
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] int score = 50;

        public int GetScore()
        {
            return score;
        }
    }
}
