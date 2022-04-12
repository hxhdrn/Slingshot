using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Attached to projectile
public class Shoot : MonoBehaviour
{
    [SerializeField] float startForce;

    Rigidbody2D rb;

    public void ShootArrow()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * startForce, ForceMode2D.Impulse);
    }

}
