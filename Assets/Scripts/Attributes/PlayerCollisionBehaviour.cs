using System.Collections;
using UnityEngine;

public class PlayerCollisionBehaviour : MonoBehaviour
{
    [SerializeField] float collisionAllowTime = 3f;
    [SerializeField] float blinkingSpeed = 0.1f;
    float timeSinceLastCollision = Mathf.Infinity;
    bool readyForCollision = true;

    // Detects most of the enemies
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && collision.collider is PolygonCollider2D && readyForCollision)
        {
            TakeDamageBehaviour();
        }
    }

    // Detects screen border
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Background")
        {
            GetComponent<Health>().Die();
        }
    }

    // Detects piranha plant
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && readyForCollision)
        {
            TakeDamageBehaviour();
        }
    }

    private void Update()
    {
        timeSinceLastCollision += Time.deltaTime;
    }

    private void TakeDamageBehaviour()
    {
        readyForCollision = false;
        gameObject.tag = "PlayerDisable";
        timeSinceLastCollision = 0f;
        GetComponent<Health>().TakeDamage(1);
        StartCoroutine(BlinkAnimation());
    }

    IEnumerator BlinkAnimation()
    {
        while (timeSinceLastCollision < collisionAllowTime)
        {
            GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.4f);
            yield return new WaitForSeconds(blinkingSpeed);
            GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 1f);
            yield return new WaitForSeconds(blinkingSpeed);
        }

        readyForCollision = true;
        gameObject.tag = "Player";
        StopCoroutine("BlinkAnimation");
    }
}
