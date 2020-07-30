using System.Collections;
using UnityEngine;

public class PlayerCollisionBehaviour : MonoBehaviour
{
    [SerializeField] float collisionAllowTime = 3f;
    [SerializeField] float blinkingSpeed = 0.1f;
    float timeSinceLastCollision = Mathf.Infinity;
    bool readyForCollision = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && collision.collider is PolygonCollider2D && readyForCollision)
        {
            readyForCollision = false;
            gameObject.tag = "PlayerDisable";
            timeSinceLastCollision = 0f;
            GetComponent<Health>().TakeDamage(1);
            StartCoroutine(BlinkAnimation());
        }
    }

    private void Update()
    {
        timeSinceLastCollision += Time.deltaTime;
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
