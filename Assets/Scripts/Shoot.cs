using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] float startSpeed;

    Rigidbody2D rb;

    public void ShootArrow()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * startSpeed, ForceMode2D.Impulse);
    }

}
