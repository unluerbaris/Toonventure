using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    Animator animator;
    GameObject audioListener;
    [SerializeField] AudioClip explosionSFX;
    [Range(0, 1)] [SerializeField] float explosionVolume = 0.25f;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioListener = GameObject.FindWithTag("AudioListener");
    }

    void Update()
    {
        Destroy(gameObject, 3f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(explosionSFX, audioListener.transform.position, explosionVolume);
            animator.SetTrigger("explode");
            Destroy(gameObject, 0.3f);
        }
    }
}
