using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    [SerializeField] GameObject bomb;

    void Start()
    {
        InvokeRepeating("ThrowBomb", 1, 1);
    }

    private void ThrowBomb()
    {
        GameObject bombInstance = Instantiate(bomb,
                                              transform.position,
                                              Quaternion.identity) as GameObject;
    }
}
