using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CTrigger : MonoBehaviour
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
        textBox.GetComponent<Text>().text = "Don't mess with Piranha Plants" + Environment.NewLine
                                          + "You only have paws !!!";
        yield return new WaitForSeconds(2.5f);
        textBox.GetComponent<Text>().text = "";
    }
}
