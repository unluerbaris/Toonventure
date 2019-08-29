using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BTrigger : MonoBehaviour
{
    [SerializeField] GameObject textBox;
    bool isTriggered = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTriggered)
        {
            isTriggered = true;
            StartCoroutine(TextPlayer());
        }
    }

    IEnumerator TextPlayer()
    {
        textBox.GetComponent<Text>().text = "To go up" + Environment.NewLine
                                          + "Climb using" + Environment.NewLine + "W Button";
        yield return new WaitForSeconds(4f);
        textBox.GetComponent<Text>().text = "";
    }
}
