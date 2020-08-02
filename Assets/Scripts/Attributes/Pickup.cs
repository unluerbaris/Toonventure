using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] int score = 50;

    public int GetScore()
    {
        return score;
    }
}
