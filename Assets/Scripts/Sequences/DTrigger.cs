using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DTrigger : MonoBehaviour
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
        textBox.GetComponent<Text>().text = "Try to jump on that Slug's head" + Environment.NewLine;
        yield return new WaitForSeconds(2.5f);
        textBox.GetComponent<Text>().text = "";
    }
}
