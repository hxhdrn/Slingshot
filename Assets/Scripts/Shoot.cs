using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Attached to projectile
public class Shoot : MonoBehaviour
{
    [SerializeField] float startForce;

    Rigidbody2D rb;
    AudioManager audioMgr;

    public void ShootArrow()
    {
        rb = GetComponent<Rigidbody2D>();
        audioMgr = FindObjectOfType<AudioManager>();

        audioMgr.PlaySound("shoot");
        rb.AddForce(transform.up * startForce, ForceMode2D.Impulse);
    }

}
