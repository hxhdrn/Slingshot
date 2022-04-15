using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] GameObject projectile; // prefab
    [SerializeField] ParticleSystem hit;

    AudioManager audioMgr;

    private void Start()
    {
        audioMgr = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == projectile.layer)
        {
            audioMgr.PlaySound("hit");
            hit.Play();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
