using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] Vector2 movementVector;
    [SerializeField] float period = 2f;
    float movementFactor;
    Vector3 startingPos;

    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) return; // protection for calcultion(period shouldn't be equal to zero)
        float cycles = Time.time / period; // grows continually from 0
        const float tau = Mathf.PI * 2; // about 6.28
        float rawSinWave = Mathf.Sin(cycles * tau); // sinwave creates values between -1 to 1
        movementFactor = (rawSinWave / 2f) + 0.5f; // movement factor updates itself between 0 to 1
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") // Player becomes a child of the platform and moves together
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") // When player jumps off from the platform, it stops to be a child of the platform
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
