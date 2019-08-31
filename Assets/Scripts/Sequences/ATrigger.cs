using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ATrigger : MonoBehaviour
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
        yield return new WaitForSeconds(1f);
        textBox.GetComponent<Text>().text = "Hey try to move" + Environment.NewLine
                                          + "Left: A" + Environment.NewLine + "Right: D"
                                          + Environment.NewLine + "Jump: SPACE";
        yield return new WaitForSeconds(6f);
        textBox.GetComponent<Text>().text = "";
    }
}
